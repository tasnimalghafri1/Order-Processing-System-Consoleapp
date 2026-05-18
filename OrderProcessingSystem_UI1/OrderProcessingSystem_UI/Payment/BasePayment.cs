using System;
using OrderProcessingSystem_UI.Interfaces;

namespace OrderProcessingSystem_UI.Payment
{
    public abstract class BasePayment : IPayable
    {
        // خصائص خاصة
        protected string transactionId;
        protected DateTime paymentDate;

        // Getters (قراءة فقط)
        public string TransactionId
        {
            get { return transactionId; }
        }

        public DateTime PaymentDate
        {
            get { return paymentDate; }
        }

        // المنشئ
        protected BasePayment()
        {
            transactionId = Guid.NewGuid().ToString();
            paymentDate = DateTime.Now;
        }

        // دوال مجردة (راح يكتبها الأبناء)
        public abstract void Pay(double amount);
        public abstract string GetPaymentMethodName();
    }
}