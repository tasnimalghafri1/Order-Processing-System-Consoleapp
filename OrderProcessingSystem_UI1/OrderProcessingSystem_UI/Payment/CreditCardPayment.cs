using System.Windows.Forms;

namespace OrderProcessingSystem_UI.Payment
{
    public class CreditCardPayment : BasePayment
    {
        private string cardNumber;
        private string cardHolderName;
        private string expiryDate;

        // خصائص
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = MaskCardNumber(value); }
        }

        public string CardHolderName
        {
            get { return cardHolderName; }
            set { cardHolderName = value; }
        }

        public string ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        // إخفاء أرقام البطاقة ما عدا آخر 4 أرقام
        private string MaskCardNumber(string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length < 4)
                return "****";
            return "****-****-****-" + number.Substring(number.Length - 4);
        }

        public override void Pay(double amount)
        {
            MessageBox.Show(
                $"💳 الدفع ببطاقة ائتمان\n\n" +
                $"المبلغ: ${amount:F2}\n" +
                $"البطاقة: {CardNumber}\n" +
                $"اسم حامل البطاقة: {CardHolderName}\n" +
                $"تاريخ الانتهاء: {ExpiryDate}\n" +
                $"رقم العملية: {TransactionId}\n" +
                $"التاريخ: {PaymentDate:yyyy-MM-dd HH:mm}\n\n" +
                $"✅ تمت الموافقة على العملية!",
                "الدفع",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public override string GetPaymentMethodName()
        {
            return "بطاقة ائتمان";
        }
    }
}