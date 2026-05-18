namespace OrderProcessingSystem.Models
{
    // Inheritance
    public class ElectronicsProduct : Product
    {
        public ElectronicsProduct(
            string id,
            string name,
            double price,
            int stockQuantity)
            : base(id, name, price, stockQuantity)
        {
        }

        // Polymorphism
        public override double CalculateDiscount()
        {
            return Price * 0.10; // 10% discount
        }
    }
}