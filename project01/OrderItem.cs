namespace OrderProcessingSystem.Models
{
    public class OrderItem
    {
        // Product inside the order
        public Product Product { get; set; }

        // Quantity of product
        public int Quantity { get; set; }

        // Calculate subtotal
        public double SubTotal
        {
            get
            {
                return (Product.Price - Product.CalculateDiscount())
                        * Quantity;
            }
        }

        // Constructor
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}