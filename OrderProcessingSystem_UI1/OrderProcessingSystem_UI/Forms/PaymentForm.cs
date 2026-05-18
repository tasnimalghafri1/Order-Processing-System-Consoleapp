using System;
using System.Drawing;
using System.Windows.Forms;
using OrderProcessingSystem_UI.Payment;

namespace OrderProcessingSystem_UI.Forms
{
    public class PaymentForm : Form
    {
        // عناصر الواجهة
        private ComboBox cmbPaymentMethod;
        private Panel paymentDetailsPanel;
        private TextBox txtCardNumber, txtCardHolder, txtExpiry, txtCVV;
        private TextBox txtPaypalEmail;
        private Button btnPay, btnCancel;
        private Label lblAmount;

        private double orderAmount;
        private BasePayment selectedPayment;

        // خاصية لاسترجاع طريقة الدفع المختارة
        public BasePayment SelectedPayment
        {
            get { return selectedPayment; }
        }

        // المنشئ
        public PaymentForm(double amount)
        {
            orderAmount = amount;
            InitializeMyForm();
            UpdatePaymentDetails();
        }

        // تصميم الواجهة
        private void InitializeMyForm()
        {
            // إعدادات النافذة
            this.Text = "💳 الدفع";
            this.Size = new Size(500, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // ======== اللوحة العلوية ========
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);

            Label lblTitle = new Label();
            lblTitle.Text = "💳 بوابة الدفع";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(20, 25);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            // ======== اللوحة الرئيسية ========
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // ======== عرض المبلغ ========
            lblAmount = new Label();
            lblAmount.Text = $"المبلغ الإجمالي: ${orderAmount:F2}";
            lblAmount.Location = new Point(0, 0);
            lblAmount.Size = new Size(440, 45);
            lblAmount.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblAmount.TextAlign = ContentAlignment.MiddleCenter;
            lblAmount.BackColor = Color.FromArgb(46, 204, 113);
            lblAmount.ForeColor = Color.White;

            // ======== اختيار طريقة الدفع ========
            GroupBox methodGroup = new GroupBox();
            methodGroup.Text = "اختر طريقة الدفع";
            methodGroup.Location = new Point(0, 55);
            methodGroup.Size = new Size(440, 70);
            methodGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            methodGroup.BackColor = Color.White;

            cmbPaymentMethod = new ComboBox();
            cmbPaymentMethod.Location = new Point(15, 28);
            cmbPaymentMethod.Size = new Size(200, 30);
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Items.AddRange(new object[] { "💵 كاش", "💳 بطاقة ائتمان", "💰 PayPal" });
            cmbPaymentMethod.SelectedIndex = 0;
            cmbPaymentMethod.SelectedIndexChanged += (s, e) => UpdatePaymentDetails();

            methodGroup.Controls.Add(cmbPaymentMethod);

            // ======== تفاصيل الدفع ========
            paymentDetailsPanel = new Panel();
            paymentDetailsPanel.Location = new Point(0, 135);
            paymentDetailsPanel.Size = new Size(440, 210);
            paymentDetailsPanel.BackColor = Color.White;

            // ----- بطاقة ائتمان -----
            Panel creditCardPanel = new Panel();
            creditCardPanel.Location = new Point(10, 10);
            creditCardPanel.Size = new Size(420, 190);
            creditCardPanel.Visible = true;

            // رقم البطاقة
            Label lblCardNumber = new Label();
            lblCardNumber.Text = "رقم البطاقة:";
            lblCardNumber.Location = new Point(10, 15);
            lblCardNumber.Size = new Size(100, 25);
            txtCardNumber = new TextBox();
            txtCardNumber.Location = new Point(120, 13);
            txtCardNumber.Size = new Size(280, 25);
            txtCardNumber.PlaceholderText = "1234 5678 9012 3456";

            // اسم حامل البطاقة
            Label lblCardHolder = new Label();
            lblCardHolder.Text = "اسم حامل البطاقة:";
            lblCardHolder.Location = new Point(10, 50);
            lblCardHolder.Size = new Size(100, 25);
            txtCardHolder = new TextBox();
            txtCardHolder.Location = new Point(120, 48);
            txtCardHolder.Size = new Size(280, 25);
            txtCardHolder.PlaceholderText = "Ahmed Ali";

            // تاريخ الانتهاء
            Label lblExpiry = new Label();
            lblExpiry.Text = "تاريخ الانتهاء:";
            lblExpiry.Location = new Point(10, 85);
            lblExpiry.Size = new Size(100, 25);
            txtExpiry = new TextBox();
            txtExpiry.Location = new Point(120, 83);
            txtExpiry.Size = new Size(100, 25);
            txtExpiry.PlaceholderText = "12/25";

            // CVV
            Label lblCVV = new Label();
            lblCVV.Text = "CVV:";
            lblCVV.Location = new Point(240, 85);
            lblCVV.Size = new Size(50, 25);
            txtCVV = new TextBox();
            txtCVV.Location = new Point(290, 83);
            txtCVV.Size = new Size(60, 25);
            txtCVV.PlaceholderText = "123";

            creditCardPanel.Controls.Add(lblCardNumber);
            creditCardPanel.Controls.Add(txtCardNumber);
            creditCardPanel.Controls.Add(lblCardHolder);
            creditCardPanel.Controls.Add(txtCardHolder);
            creditCardPanel.Controls.Add(lblExpiry);
            creditCardPanel.Controls.Add(txtExpiry);
            creditCardPanel.Controls.Add(lblCVV);
            creditCardPanel.Controls.Add(txtCVV);

            // ----- PayPal -----
            Panel paypalPanel = new Panel();
            paypalPanel.Location = new Point(10, 10);
            paypalPanel.Size = new Size(420, 190);
            paypalPanel.Visible = false;

            Label lblPaypalEmail = new Label();
            lblPaypalEmail.Text = "البريد الإلكتروني PayPal:";
            lblPaypalEmail.Location = new Point(10, 15);
            lblPaypalEmail.Size = new Size(150, 25);
            txtPaypalEmail = new TextBox();
            txtPaypalEmail.Location = new Point(170, 13);
            txtPaypalEmail.Size = new Size(230, 25);
            txtPaypalEmail.PlaceholderText = "ahmed@example.com";

            Label lblPaypalNote = new Label();
            lblPaypalNote.Text = "سيتم تحويلك إلى PayPal لإتمام الدفع.";
            lblPaypalNote.Location = new Point(10, 60);
            lblPaypalNote.Size = new Size(400, 30);
            lblPaypalNote.ForeColor = Color.Gray;

            paypalPanel.Controls.Add(lblPaypalEmail);
            paypalPanel.Controls.Add(txtPaypalEmail);
            paypalPanel.Controls.Add(lblPaypalNote);

            // ----- كاش -----
            Panel cashPanel = new Panel();
            cashPanel.Location = new Point(10, 10);
            cashPanel.Size = new Size(420, 190);
            cashPanel.Visible = false;

            Label lblCashNote = new Label();
            lblCashNote.Text = "الرجاء تجهيز المبلغ المطلوب نقداً.\nلن يتم إرجاع باقي المبلغ.";
            lblCashNote.Location = new Point(10, 15);
            lblCashNote.Size = new Size(400, 60);
            lblCashNote.ForeColor = Color.Gray;
            lblCashNote.TextAlign = ContentAlignment.MiddleCenter;
            lblCashNote.Font = new Font("Segoe UI", 10);

            cashPanel.Controls.Add(lblCashNote);

            paymentDetailsPanel.Controls.Add(creditCardPanel);
            paymentDetailsPanel.Controls.Add(paypalPanel);
            paymentDetailsPanel.Controls.Add(cashPanel);

            // ======== الأزرار ========
            btnPay = new Button();
            btnPay.Text = "✅ تأكيد الدفع";
            btnPay.Location = new Point(0, 355);
            btnPay.Size = new Size(215, 45);
            btnPay.BackColor = Color.FromArgb(46, 204, 113);
            btnPay.ForeColor = Color.White;
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnPay.Click += BtnPay_Click;

            btnCancel = new Button();
            btnCancel.Text = "❌ إلغاء";
            btnCancel.Location = new Point(225, 355);
            btnCancel.Size = new Size(215, 45);
            btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // إضافة كل شيء
            mainPanel.Controls.Add(lblAmount);
            mainPanel.Controls.Add(methodGroup);
            mainPanel.Controls.Add(paymentDetailsPanel);
            mainPanel.Controls.Add(btnPay);
            mainPanel.Controls.Add(btnCancel);

            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
        }

        // إظهار/إخفاء تفاصيل الدفع حسب الطريقة المختارة
        private void UpdatePaymentDetails()
        {
            // إخفاء كل اللوحات
            foreach (Control panel in paymentDetailsPanel.Controls)
            {
                panel.Visible = false;
            }

            string selected = cmbPaymentMethod.SelectedItem.ToString();

            if (selected.Contains("بطاقة"))
            {
                paymentDetailsPanel.Controls[0].Visible = true; // بطاقة ائتمان
            }
            else if (selected.Contains("PayPal"))
            {
                paymentDetailsPanel.Controls[1].Visible = true; // PayPal
            }
            else if (selected.Contains("كاش"))
            {
                paymentDetailsPanel.Controls[2].Visible = true; // كاش
            }
        }

        // تأكيد الدفع
        private void BtnPay_Click(object sender, EventArgs e)
        {
            string selected = cmbPaymentMethod.SelectedItem.ToString();

            try
            {
                if (selected.Contains("بطاقة"))
                {
                    // التحقق من صحة البيانات
                    if (string.IsNullOrWhiteSpace(txtCardNumber.Text) ||
                        string.IsNullOrWhiteSpace(txtCardHolder.Text) ||
                        string.IsNullOrWhiteSpace(txtExpiry.Text) ||
                        string.IsNullOrWhiteSpace(txtCVV.Text))
                    {
                        MessageBox.Show("الرجاء إدخال جميع بيانات البطاقة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CreditCardPayment creditCard = new CreditCardPayment();
                    creditCard.CardNumber = txtCardNumber.Text;
                    creditCard.CardHolderName = txtCardHolder.Text;
                    creditCard.ExpiryDate = txtExpiry.Text;
                    selectedPayment = creditCard;
                }
                else if (selected.Contains("PayPal"))
                {
                    // التحقق من صحة البيانات
                    if (string.IsNullOrWhiteSpace(txtPaypalEmail.Text))
                    {
                        MessageBox.Show("الرجاء إدخال البريد الإلكتروني PayPal.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    PaypalPayment paypal = new PaypalPayment();
                    paypal.Email = txtPaypalEmail.Text;
                    selectedPayment = paypal;
                }
                else if (selected.Contains("كاش"))
                {
                    selectedPayment = new CashPayment();
                }

                // تنفيذ الدفع
                selectedPayment.Pay(orderAmount);

                // إغلاق النافذة بنجاح
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}