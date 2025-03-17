namespace ProvaPub.Services.Payments;

public class PayPalPaymentService : IPaymentService
{
    public bool Process(int customerId, decimal value)
    {
        return true;
    }
}
