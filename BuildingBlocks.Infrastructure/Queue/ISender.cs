namespace BuildingBlocks.Infrastructure.Queue
{
    public interface ISender
    {
        Task SendMessageAsync(string message);
    }
}