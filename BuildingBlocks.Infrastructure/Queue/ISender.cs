namespace BuildingBlocks.Infrastructure.Queue
{
    public interface ISender
    {
        Task SendMessageAsync(string queueName, string message, string key);
    }
}