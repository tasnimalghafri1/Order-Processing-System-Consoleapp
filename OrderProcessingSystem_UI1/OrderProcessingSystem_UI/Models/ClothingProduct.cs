using OrderProcessingSystem_UI.Models;

namespace OrderProcessingSystem_UI.Models
{
    public class ClothingProduct : Product
    {
        // خصائص إضافية للملابس
        private string size;
        private string color;
        private string material;

        // Getters and Setters
        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Material
        {
            get { return material; }
            set { material = value; }
        }

        // المنشئ (Constructor)
        public ClothingProduct(string id, string name, double price, int stockQuantity,
            string size = "M", string color = "Black", string material = "Cotton", string description = "")
            : base(id, name, price, stockQuantity, description)
        {
            Size = size;
            Color = color;
            Material = material;
        }

        // تطبيق الدوال المجردة من Product
        public override double CalculateDiscount()
        {
            // الملابس خصم 20%
            return Price * 0.20;
        }

        public override string GetCategory()
        {
            return "Clothing";
        }

        // عرض المنتج بشكل مرتب
        public override string ToString()
        {
            return $"{Name} - ${Price:F2} (Clothing) - Size: {Size} - Color: {Color} - Stock: {StockQuantity}";
        }
    }
}