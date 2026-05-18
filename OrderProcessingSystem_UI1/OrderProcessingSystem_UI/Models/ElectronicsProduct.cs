using OrderProcessingSystem_UI.Models;

namespace OrderProcessingSystem_UI.Models
{
    public class ElectronicsProduct : Product
    {
        // خصائص إضافية للمنتج الإلكتروني
        private int warrantyMonths;
        private string brand;

        // Getters and Setters
        public int WarrantyMonths
        {
            get { return warrantyMonths; }
            set { warrantyMonths = value; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        // المنشئ (Constructor)
        public ElectronicsProduct(string id, string name, double price, int stockQuantity,
            string brand = "", int warrantyMonths = 12, string description = "")
            : base(id, name, price, stockQuantity, description)
        {
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        // تطبيق الدوال المجردة من Product
        public override double CalculateDiscount()
        {
            // الإلكترونيات خصم 10%
            return Price * 0.10;
        }

        public override string GetCategory()
        {
            return "Electronics";
        }

        // عرض المنتج بشكل مرتب
        public override string ToString()
        {
            string brandInfo = string.IsNullOrEmpty(Brand) ? "" : $" [{Brand}]";
            return $"{Name}{brandInfo} - ${Price:F2} (Electronics) - Warranty: {WarrantyMonths}m - Stock: {StockQuantity}";
        }
    }
}