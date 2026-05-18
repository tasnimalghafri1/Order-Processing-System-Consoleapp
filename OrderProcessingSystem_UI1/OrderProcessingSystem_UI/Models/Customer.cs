using System.Collections.Generic;

namespace OrderProcessingSystem_UI.Models
{
    public class Customer
    {
        // 1. الخصائص الخاصة (private)
        private string id;
        private string name;
        private string email;
        private string phone;
        private List<Order> orders;

        // 2. Getters and Setters (public)
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

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        // 3. المنشئ (Constructor)
        public Customer(string id, string name, string email, string phone = "")
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Orders = new List<Order>();
        }

        // 4. لعرض العميل بشكل مرتب
        public override string ToString()
        {
            return $"{Name} - {Email}";
        }
    }
}