using BuildingBlocks.Infrastructure.Queue;

namespace Order.Infrastructure.JobScheduling
{
    public class RaportGenerator(IConsumer consumer): IRaportGenerator
    {
        private readonly IConsumer _consumer = consumer;
        public async Task GenerateRaprot()
        {
            //messages will be delivered by an independent script called e.g.by CI/ CD but it can also run all the time based on e.g.hangfier
            //data provade from configuration
            await _consumer.ConsumeMessageAsync("RaportGenerate","someKeyformConfig", async (model, ea) =>
            {
                //in body we have only customerId
                //Deserialese
                //get data form OrderRepo 
                //Generate PDF and send by email provider
            });
        }
    }
}
