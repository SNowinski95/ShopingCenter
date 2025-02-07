namespace Order.Application.Commends.Create
{
    public enum ResultStatus
    {
        Success,
        Fail
    }

    public record CreateOrderResult(ResultStatus ResultStatus, string Message = "")
    {
    }
}
