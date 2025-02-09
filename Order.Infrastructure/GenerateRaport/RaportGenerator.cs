using BuildingBlocks.Infrastructure.Queue;
using Order.Domain;
using Order.Infrastructure.GenerateRaport.EmailProvider;
using Order.Infrastructure.GenerateRaport.FileManagmet;
using System.Text;

namespace Order.Infrastructure.JobScheduling
{
    public class RaportGenerator(IConsumer consumer,IOrderRepository orderRepository, TimeProvider timeProvider): IRaportGenerator
    {
        private readonly IConsumer _consumer = consumer;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly TimeProvider _timeProvider = timeProvider;
        private const string TempFolder = "someTempPathfromConfig";

        public async Task GenerateRaprot()
        {
            //messages will be delivered by an independent script called e.g.by CI/ CD but it can also run all the time based on e.g.hangfier
            //data provade from configuration
            await _consumer.ConsumeMessageAsync("RaportGenerate","someKeyformConfig", async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var cutomerId = Encoding.UTF8.GetString(body);
                var month = _timeProvider.GetUtcNow().Month;
                var year = _timeProvider.GetUtcNow().Year;
                var orders = await _orderRepository.GetFiteredAsync(cutomerId, Order.Domain.ValueObjects.PaymentStatus.Paid, new DateTime(year,month,1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
                PDFCreator.GenerateDoc(orders, TempFolder);
                await EmailSender.SendFile("someRecverFromUserApi", TempFolder);
            });
        }
    }
}
