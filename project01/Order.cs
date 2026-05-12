using OrderProcessingSystem.Enums;
using OrderProcessingSystem.Payments;
using project01;
using project01.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessingSystem.Models
{
    public class Order
    {
        // Properties
        public string Id { get; set; }

        public Customer Customer { get; set; }

        public List<OrderItem> Items { get; set; }

        public OrderStatus Status { get; set; }

        public Payment PaymentMethod { get; set; }

        public DateTime OrderDate { get; set; }

        // Constructor
        public Order(string id, Customer customer)
        {
            Id = id;
            Customer = customer;

            Items = new List<OrderItem>();

            Status = OrderStatus.Pending;

            OrderDate = DateTime.Now;
        }

        // Add product to order
        public void AddProduct(Product product, int quantity)
        {
            if (quantity <= product.StockQuantity)
            {
                Items.Add(new OrderItem(product, quantity));

                product.StockQuantity -= quantity;

                Console.WriteLine($"{product.Name} added to order.");
            }
            else
            {
                Console.WriteLine($"Not enough stock for {product.Name}");
            }
        }

        // Calculate totall
        public double CalculateTotal()
        {
            return Items.Sum(item => item.SubTotal);
        }

        // Set payment method
        public void SetPayment(Payment payment)
        {
            PaymentMethod = payment;
        }

        // Process order
        public void ProcessOrder()
        {
            Status = OrderStatus.Processing;

            double total = CalculateTotal();

            PaymentMethod.Pay(total);

            Status = OrderStatus.Shipped;
        }

        // Print order summary
        public void PrintSummary()
        {
            Console.WriteLine("\n===== ORDER SUMMARY =====");

            Console.WriteLine($"Order ID: {Id}");

            Console.WriteLine($"Customer: {Customer.Name}");

            Console.WriteLine($"Status: {Status}");

            Console.WriteLine($"Date: {OrderDate}");

            Console.WriteLine("\nProducts:");

            foreach (var item in Items)
            {
                Console.WriteLine(
                    $"{item.Product.Name} | " +
                    $"Qty: {item.Quantity} | " +
                    $"Subtotal: ${item.SubTotal}"
                );
            }

            Console.WriteLine("-------------------------");

            Console.WriteLine($"Total: ${CalculateTotal()}");
        }
    }
}