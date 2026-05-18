using System;
using System.Collections.Generic;
using System.Linq;
using OrderProcessingSystem_UI.Interfaces;
using OrderProcessingSystem_UI.Payment;

namespace OrderProcessingSystem_UI.Models
{
    // حالة الطلب (enum)
    public enum OrderStatus
    {
        Pending,      // قيد الانتظار
        Processing,   // قيد المعالجة
        Shipped,      // تم الشحن
        Delivered,    // تم التسليم
        Cancelled     // ملغي
    }

    public class Order : IShippable
    {
        // الخصائص الخاصة
        private string id;
        private Customer customer;
        private List<OrderItem> items;
        private double totalPrice;
        private OrderStatus status;
        private BasePayment paymentMethod;
        private DateTime orderDate;
        private DateTime? shippedDate;
        private DateTime? deliveredDate;

        // Getters and Setters
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public List<OrderItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public OrderStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public BasePayment PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public DateTime? ShippedDate
        {
            get { return shippedDate; }
            set { shippedDate = value; }
        }

        public DateTime? DeliveredDate
        {
            get { return deliveredDate; }
            set { deliveredDate = value; }
        }

        public int ItemCount
        {
            get { return items?.Count ?? 0; }
        }

        // المنشئ
        public Order(string id, Customer customer)
        {
            Id = id;
            Customer = customer;
            Items = new List<OrderItem>();
            Status = OrderStatus.Pending;
            OrderDate = DateTime.Now;
            TotalPrice = 0;
        }

        // إضافة منتج إلى الطلب
        public void AddItem(Product product, int quantity)
        {
            // التحقق من وجود كمية كافية في المخزون
            if (product.StockQuantity < quantity)
            {
                throw new Exception($"الكمية المطلوبة غير متوفرة للمنتج {product.Name}. المتوفر: {product.StockQuantity}");
            }

            // هل المنتج موجود مسبقاً في الطلب؟
            var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existingItem != null)
            {
                // إذا كان موجوداً، زود الكمية
                existingItem.Quantity += quantity;
                // إعادة حساب المجموع
                double discount = existingItem.Product.CalculateDiscount();
                existingItem.DiscountApplied = discount * existingItem.Quantity;
                existingItem.SubTotal = (existingItem.Product.Price * existingItem.Quantity) - existingItem.DiscountApplied;
            }
            else
            {
                // إذا كان جديداً، أضفه
                Items.Add(new OrderItem(product, quantity));
            }

            // إنقاص الكمية من المخزون
            product.StockQuantity -= quantity;

            // إعادة حساب المجموع الكلي
            CalculateTotal();
        }

        // حذف منتج من الطلب
        public void RemoveItem(string productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                // إعادة الكمية إلى المخزون
                item.Product.StockQuantity += item.Quantity;

                // حذف المنتج
                Items.Remove(item);

                // إعادة حساب المجموع الكلي
                CalculateTotal();
            }
        }

        // حساب المجموع الكلي
        public void CalculateTotal()
        {
            TotalPrice = Items.Sum(i => i.SubTotal);
        }

        // تعيين طريقة الدفع
        public void SetPayment(BasePayment payment)
        {
            PaymentMethod = payment;
        }

        // معالجة الطلب
        public void ProcessOrder()
        {
            if (PaymentMethod == null)
            {
                throw new Exception("الرجاء اختيار طريقة الدفع أولاً.");
            }

            // تغيير الحالة إلى قيد المعالجة
            Status = OrderStatus.Processing;

            // تنفيذ الدفع
            PaymentMethod.Pay(TotalPrice);

            // تغيير الحالة إلى تم الشحن
            Status = OrderStatus.Shipped;
            ShippedDate = DateTime.Now;

            // تنفيذ الشحن
            Ship();
        }

        // تنفيذ الشحن (من IShippable)
        public void Ship()
        {
            Status = OrderStatus.Shipped;
            ShippedDate = DateTime.Now;
        }

        // تسليم الطلب
        public void Deliver()
        {
            Status = OrderStatus.Delivered;
            DeliveredDate = DateTime.Now;
        }

        // إلغاء الطلب
        public void Cancel()
        {
            Status = OrderStatus.Cancelled;

            // إعادة الكميات إلى المخزون
            foreach (var item in Items)
            {
                item.Product.StockQuantity += item.Quantity;
            }
        }

        // الحصول على حالة الشحن (من IShippable)
        public string GetShippingStatus()
        {
            switch (Status)
            {
                case OrderStatus.Pending:
                    return "في انتظار الدفع";
                case OrderStatus.Processing:
                    return "جاري تجهيز الطلب";
                case OrderStatus.Shipped:
                    return $"تم الشحن بتاريخ {ShippedDate:yyyy-MM-dd}";
                case OrderStatus.Delivered:
                    return $"تم التسليم بتاريخ {DeliveredDate:yyyy-MM-dd}";
                case OrderStatus.Cancelled:
                    return "تم إلغاء الطلب";
                default:
                    return "حالة غير معروفة";
            }
        }

        // عرض ملخص الطلب
        public string GetOrderSummary()
        {
            string summary = $"═══════════════════════════════\n";
            summary += $"         ملخص الطلب\n";
            summary += $"═══════════════════════════════\n";
            summary += $"رقم الطلب: {Id}\n";
            summary += $"العميل: {Customer.Name}\n";
            summary += $"التاريخ: {OrderDate:yyyy-MM-dd HH:mm}\n";
            summary += $"الحالة: {GetShippingStatus()}\n";
            summary += $"\nالمنتجات:\n";

            foreach (var item in Items)
            {
                summary += $"  • {item.Product.Name} x{item.Quantity} = ${item.SubTotal:F2}\n";
                if (item.DiscountApplied > 0)
                {
                    summary += $"    (خصم: ${item.DiscountApplied:F2})\n";
                }
            }

            summary += $"\n───────────────────────────────\n";
            summary += $"المجموع: ${TotalPrice:F2}\n";
            summary += $"طريقة الدفع: {PaymentMethod?.GetPaymentMethodName() ?? "غير محدد"}\n";
            summary += $"═══════════════════════════════";

            return summary;
        }
    }
}