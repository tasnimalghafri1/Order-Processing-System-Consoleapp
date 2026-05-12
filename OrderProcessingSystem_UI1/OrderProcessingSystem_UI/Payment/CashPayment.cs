using System.Windows.Forms;

namespace OrderProcessingSystem_UI.Payment
{
    public class CashPayment : BasePayment
    {
        public override void Pay(double amount)
        {
            MessageBox.Show(
                $"💵 الدفع كاش\n\n" +
                $"المبلغ: ${amount:F2}\n" +
                $"رقم العملية: {TransactionId}\n" +
                $"التاريخ: {PaymentDate:yyyy-MM-dd HH:mm}\n\n" +
                $"الرجاء تجهيز المبلغ المطلوب.",
                "الدفع",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public override string GetPaymentMethodName()
        {
            return "كاش";
        }
    }
}