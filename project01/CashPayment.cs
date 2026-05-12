using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Payments
{
    public class CashPayment : Payment
    {
        public override void Pay(double amount)
        {
            Console.WriteLine($"Cash Payment Successful: {amount}$");
        }
    }
}