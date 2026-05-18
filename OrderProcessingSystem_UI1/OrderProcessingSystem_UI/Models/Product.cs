namespace OrderProcessingSystem_UI.Models
{
    public abstract class Product
    {
        // الخصائص الخاصة
        private string id;
        private string name;
        private double price;
        private int stockQuantity;
        private string description;

        // Getters and Setters
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

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // المنشئ
        protected Product(string id, string name, double price, int stockQuantity, string description = "")
        {
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            Description = description;
        }

        // دوال مجردة (راح يكتبها الأبناء)
        public abstract double CalculateDiscount();
        public abstract string GetCategory();

        // عرض المنتج
        public override string ToString()
        {
            return $"{Name} - ${Price:F2} ({GetCategory()}) - Stock: {StockQuantity}";
        }
    }
}