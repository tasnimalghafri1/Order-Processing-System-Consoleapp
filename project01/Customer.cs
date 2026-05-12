using System.Collections.Generic;

namespace OrderProcessingSystem.Models
{
    public class Customer
    {
        // Encapsulation
        private string id;
        private string name;
        private string email;

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

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        // Orders List
        public List<Order> Orders { get; set; }

        // Constructor
        public Customer(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;

            Orders = new List<Order>();
        }
    }
}