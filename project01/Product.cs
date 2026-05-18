namespace OrderProcessingSystem.Models
{
    public class Product
    {
        // Encapsulation
        private string id;
        private string name;
        private double price;
        private int stockQuantity;

        // Properties
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }

        // Constructor
        public Product(string id, string name, double price, int stockQuantity)
        {
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
        }

        // Polymorphism
        public virtual double CalculateDiscount()
        {
            return 0;
        }
    }
}