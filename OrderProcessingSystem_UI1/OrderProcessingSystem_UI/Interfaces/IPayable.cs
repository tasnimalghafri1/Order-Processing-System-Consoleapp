namespace OrderProcessingSystem_UI.Interfaces
{
    public interface IPayable
    {
        void Pay(double amount);
        string GetPaymentMethodName();
    }
}