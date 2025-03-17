namespace ProvaPub.Services.Payments;

public class CreditCardPaymentService : IPaymentService
{
    public bool Process(int customerId, decimal value)
    {
        return true;
    }
}
