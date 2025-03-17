namespace ProvaPub.Services.Payments;

public class PaymentServiceFactory
{
    private readonly Dictionary<string, IPaymentService> _services;

    public PaymentServiceFactory()
    {
        _services = new Dictionary<string, IPaymentService>()
        {
            {"pix", new PixPaymentService() },
            {"creditcard", new CreditCardPaymentService() },
            {"paypal", new PayPalPaymentService() },
        };
    }
    public IPaymentService GetPaymentService(string method)
    {
        if (_services.TryGetValue(method.ToLower(), out var service))
            return service;
        throw new InvalidOperationException("Metodo de pagamento nõa encontrado");
    }
}
