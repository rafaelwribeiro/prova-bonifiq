namespace ProvaPub.Services.Payments;

public interface IPaymentService
{
    public bool Process(int customerId, decimal value);
}
