namespace Order.Domain.ValueObjects
{
    public enum PaymentStatus
    {
        WaitingForReservation,
        WaitingForPayment,
        Paid,
        Fail
    }
}
