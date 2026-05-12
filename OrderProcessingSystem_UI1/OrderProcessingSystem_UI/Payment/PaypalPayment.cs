using System.Windows.Forms;

namespace OrderProcessingSystem_UI.Payment
{
    public class PaypalPayment : BasePayment
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public override void Pay(double amount)
        {
            MessageBox.Show(
                $"💰 الدفع عبر PayPal\n\n" +
                $"المبلغ: ${amount:F2}\n" +
                $"البريد الإلكتروني: {Email}\n" +
                $"رقم العملية: {TransactionId}\n" +
                $"التاريخ: {PaymentDate:yyyy-MM-dd HH:mm}\n\n" +
                $"تم إرسال إيصال الدفع إلى بريدك الإلكتروني.",
                "الدفع",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public override string GetPaymentMethodName()
        {
            return "PayPal";
        }
    }
}