namespace OrderProcessingSystem_UI.Models
{
    public class OrderItem
    {
        // الخصائص الخاصة
        private Product product;
        private int quantity;
        private double subTotal;
        private double discountApplied;

        // Getters and Setters
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }

        public double DiscountApplied
        {
            get { return discountApplied; }
            set { discountApplied = value; }
        }

        // المنشئ - يحسب الخصم والمجموع تلقائياً
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            double discount = product.CalculateDiscount();
            DiscountApplied = discount * quantity;
            SubTotal = (product.Price * quantity) - DiscountApplied;
        }
    }
}