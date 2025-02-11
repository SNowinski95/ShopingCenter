using BuildingBlocks.Infrastructure.Queue;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain;
using Order.Infrastructure.GenerateRaport.EmailProvider;
using Order.Infrastructure.GenerateRaport.FileManagmet;
using System.Text;

namespace Order.Infrastructure.JobScheduling
{
    public class RaportGenerator: IRaportGenerator
    {
        private readonly IConsumer _consumer;
        private readonly TimeProvider _timeProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RaportGenerator(IConsumer consumer,TimeProvider timeProvider, IServiceScopeFactory serviceScopeFactory)
        {
            _consumer = consumer;
            _timeProvider = timeProvider;
            _serviceScopeFactory = serviceScopeFactory;
        }

        private const string TempFolder = "someTempPathfromConfig";

        public async Task GenerateRaprot()
        {
            //messages will be delivered by an independent script called e.g.by CI/ CD but it can also run all the time based on e.g.hangfier
            //data provade from configuration
            await _consumer.ConsumeMessageAsync(async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var cutomerId = Encoding.UTF8.GetString(body);
                var month = _timeProvider.GetUtcNow().Month;
                var year = _timeProvider.GetUtcNow().Year;
                using var scope = _serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var orders = await repo.GetFiteredAsync(cutomerId, Order.Domain.ValueObjects.PaymentStatus.Paid, new DateTime(year,month,1), new DateTime(year, month, DateTime.DaysInMonth(year, month)));
                PDFCreator.GenerateDoc(orders, TempFolder);
                await EmailSender.SendFile("someRecverFromUserApi@xx.xx", TempFolder);
                //Delete all from temp
            });
        }
    }
}
