using OrderProcessingSystem.Interfaces;

namespace OrderProcessingSystem.Payments
{
    public abstract class Payment : IPayable
    {
        public abstract void Pay(double amount);
    }
}