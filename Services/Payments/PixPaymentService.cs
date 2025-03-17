namespace ProvaPub.Services.Payments;

public class PixPaymentService : IPaymentService
{
    public bool Process(int customerId, decimal value)
    {
        return true;
    }
}
