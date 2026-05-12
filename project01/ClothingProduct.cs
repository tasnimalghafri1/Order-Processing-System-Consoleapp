namespace OrderProcessingSystem.Models
{
    // Inheritance
    public class ClothingProduct : Product
    {
        public ClothingProduct(
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
            return Price * 0.20; // 20% Discount
        }
    }
}