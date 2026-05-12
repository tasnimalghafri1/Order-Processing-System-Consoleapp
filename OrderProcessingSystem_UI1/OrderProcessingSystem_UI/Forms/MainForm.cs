using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OrderProcessingSystem_UI.Models;
using OrderProcessingSystem_UI.Payment;

namespace OrderProcessingSystem_UI.Forms
{
    public class MainForm : Form
    {
        // قوائم البيانات
        private List<Customer> customers;
        private List<Product> products;
        private Order currentOrder;

        // عناصر الواجهة
        private DataGridView dgvProducts;
        private ListView lvCart;
        private ComboBox cmbCustomers;
        private NumericUpDown nudQuantity;
        private Label lblTotal, lblCartCount, lblStatus;
        private Button btnAddToCart, btnRemoveFromCart, btnCheckout;
        private Button btnManageCustomers, btnManageProducts;

        // المنشئ
        public MainForm()
        {
            InitializeData();
            InitializeMyForm();
            LoadCustomers();
            LoadProducts();
            UpdateCartUI();
        }

        // تهيئة البيانات التجريبية
        private void InitializeData()
        {
            customers = new List<Customer>();
            products = new List<Product>();

            // إضافة عملاء تجريبيين
            customers.Add(new Customer("1", "أحمد المنصوري", "ahmed@email.com", "971501234567"));
            customers.Add(new Customer("2", "فاطمة الزهراء", "fatima@email.com", "971502345678"));

            // إضافة منتجات تجريبية
            products.Add(new ElectronicsProduct("P1", "iPhone 15 Pro", 4999, 10, "Apple", 12));
            products.Add(new ElectronicsProduct("P2", "Samsung Galaxy S24", 4299, 8, "Samsung", 12));
            products.Add(new ClothingProduct("P3", "جاكيت شتوي", 599, 20, "L", "أزرق داكن"));
            products.Add(new ClothingProduct("P4", "تي شيرت رياضي", 129, 50, "M", "أبيض"));
        }

        // تصميم الواجهة الرئيسية
        private void InitializeMyForm()
        {
            // إعدادات النافذة
            this.Text = "🛒 نظام معالجة الطلبات";
            this.Size = new Size(1300, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1000, 600);

            // ======== اللوحة العلوية ========
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);

            Label lblTitle = new Label();
            lblTitle.Text = "🛒 نظام معالجة الطلبات";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;

            lblStatus = new Label();
            lblStatus.Text = "✓ جاهز";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
            lblStatus.Font = new Font("Segoe UI", 10);
            lblStatus.Location = new Point(20, 50);
            lblStatus.AutoSize = true;

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblStatus);

            // ======== اللوحة الرئيسية ========
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);
            mainPanel.AutoScroll = true;

            // ======== بطاقة اختيار العميل ========
            Panel customerCard = CreateCard("👤 العميل", 0, 0, 550, 100);

            Label lblCustomer = new Label();
            lblCustomer.Text = "اختر العميل:";
            lblCustomer.Location = new Point(15, 35);
            lblCustomer.Size = new Size(100, 25);

            cmbCustomers = new ComboBox();
            cmbCustomers.Location = new Point(120, 33);
            cmbCustomers.Size = new Size(250, 30);
            cmbCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomers.Font = new Font("Segoe UI", 10);
            cmbCustomers.SelectedIndexChanged += (s, e) =>
            {
                currentOrder = null;
                UpdateCartUI();
                lblStatus.Text = "✓ تم تغيير العميل، يمكن بدء طلب جديد";
            };

            btnManageCustomers = new Button();
            btnManageCustomers.Text = "📋 إدارة العملاء";
            btnManageCustomers.Location = new Point(380, 32);
            btnManageCustomers.Size = new Size(150, 35);
            btnManageCustomers.BackColor = Color.FromArgb(52, 152, 219);
            btnManageCustomers.ForeColor = Color.White;
            btnManageCustomers.FlatStyle = FlatStyle.Flat;
            btnManageCustomers.Click += BtnManageCustomers_Click;

            customerCard.Controls.Add(lblCustomer);
            customerCard.Controls.Add(cmbCustomers);
            customerCard.Controls.Add(btnManageCustomers);

            // ======== بطاقة المنتجات ========
            Panel productsCard = CreateCard("📦 المنتجات", 0, 110, 850, 400);

            dgvProducts = new DataGridView();
            dgvProducts.Location = new Point(15, 30);
            dgvProducts.Size = new Size(820, 280);
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.Font = new Font("Segoe UI", 9);
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Label lblQuantity = new Label();
            lblQuantity.Text = "الكمية:";
            lblQuantity.Location = new Point(15, 325);
            lblQuantity.Size = new Size(60, 30);

            nudQuantity = new NumericUpDown();
            nudQuantity.Location = new Point(80, 323);
            nudQuantity.Size = new Size(80, 30);
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 999;
            nudQuantity.Value = 1;

            btnAddToCart = new Button();
            btnAddToCart.Text = "➕ إضافة إلى السلة";
            btnAddToCart.Location = new Point(170, 322);
            btnAddToCart.Size = new Size(150, 35);
            btnAddToCart.BackColor = Color.FromArgb(46, 204, 113);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAddToCart.Click += BtnAddToCart_Click;

            btnManageProducts = new Button();
            btnManageProducts.Text = "📦 إدارة المنتجات";
            btnManageProducts.Location = new Point(680, 322);
            btnManageProducts.Size = new Size(150, 35);
            btnManageProducts.BackColor = Color.FromArgb(52, 152, 219);
            btnManageProducts.ForeColor = Color.White;
            btnManageProducts.FlatStyle = FlatStyle.Flat;
            btnManageProducts.Click += BtnManageProducts_Click;

            productsCard.Controls.Add(dgvProducts);
            productsCard.Controls.Add(lblQuantity);
            productsCard.Controls.Add(nudQuantity);
            productsCard.Controls.Add(btnAddToCart);
            productsCard.Controls.Add(btnManageProducts);

            // ======== بطاقة سلة التسوق ========
            Panel cartCard = CreateCard("🛒 سلة التسوق", 860, 0, 400, 350);

            lvCart = new ListView();
            lvCart.Location = new Point(15, 30);
            lvCart.Size = new Size(370, 270);
            lvCart.View = View.Details;
            lvCart.FullRowSelect = true;
            lvCart.Font = new Font("Segoe UI", 9);
            lvCart.Columns.Add("المنتج", 150);
            lvCart.Columns.Add("الكمية", 60);
            lvCart.Columns.Add("السعر", 70);
            lvCart.Columns.Add("المجموع", 80);

            btnRemoveFromCart = new Button();
            btnRemoveFromCart.Text = "❌ حذف المحدد";
            btnRemoveFromCart.Location = new Point(15, 305);
            btnRemoveFromCart.Size = new Size(130, 35);
            btnRemoveFromCart.BackColor = Color.FromArgb(231, 76, 60);
            btnRemoveFromCart.ForeColor = Color.White;
            btnRemoveFromCart.FlatStyle = FlatStyle.Flat;
            btnRemoveFromCart.Click += BtnRemoveFromCart_Click;

            cartCard.Controls.Add(lvCart);
            cartCard.Controls.Add(btnRemoveFromCart);

            // ======== بطاقة الملخص ========
            Panel summaryCard = CreateCard("💰 ملخص الطلب", 860, 360, 400, 170);

            lblTotal = new Label();
            lblTotal.Text = "المجموع: $0.00";
            lblTotal.Location = new Point(15, 25);
            lblTotal.Size = new Size(370, 45);
            lblTotal.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.BackColor = Color.FromArgb(46, 204, 113);
            lblTotal.ForeColor = Color.White;

            lblCartCount = new Label();
            lblCartCount.Text = "عدد القطع: 0";
            lblCartCount.Location = new Point(15, 80);
            lblCartCount.Size = new Size(370, 25);
            lblCartCount.Font = new Font("Segoe UI", 10);
            lblCartCount.TextAlign = ContentAlignment.MiddleRight;
            lblCartCount.ForeColor = Color.Gray;

            btnCheckout = new Button();
            btnCheckout.Text = "✅ إتمام الشراء";
            btnCheckout.Location = new Point(15, 115);
            btnCheckout.Size = new Size(370, 45);
            btnCheckout.BackColor = Color.FromArgb(52, 152, 219);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCheckout.Click += BtnCheckout_Click;

            summaryCard.Controls.Add(lblTotal);
            summaryCard.Controls.Add(lblCartCount);
            summaryCard.Controls.Add(btnCheckout);

            // إضافة البطاقات إلى اللوحة الرئيسية
            mainPanel.Controls.Add(customerCard);
            mainPanel.Controls.Add(productsCard);
            mainPanel.Controls.Add(cartCard);
            mainPanel.Controls.Add(summaryCard);

            // إضافة اللوحات إلى النافذة
            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
        }

        // إنشاء بطاقة (Card) بتصميم جميل
        private Panel CreateCard(string title, int x, int y, int width, int height)
        {
            Panel card = new Panel();
            card.Location = new Point(x, y);
            card.Size = new Size(width, height);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Location = new Point(10, 5);
            lblTitle.Size = new Size(width - 20, 25);
            lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(52, 73, 94);

            card.Controls.Add(lblTitle);
            return card;
        }

        // تحميل العملاء في القائمة المنسدلة
        private void LoadCustomers()
        {
            cmbCustomers.Items.Clear();
            foreach (var c in customers)
            {
                cmbCustomers.Items.Add(c);
            }
            if (cmbCustomers.Items.Count > 0)
            {
                cmbCustomers.SelectedIndex = 0;
            }
        }

        // تحميل المنتجات في الجدول
        private void LoadProducts()
        {
            if (dgvProducts == null) return;

            var productList = products.Select(p => new
            {
                p.Id,
                p.Name,
                Price = p.Price.ToString("F2"),
                Category = p.GetCategory(),
                p.StockQuantity,
                Discount = $"{p.CalculateDiscount():F2}"
            }).ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = productList;

            if (dgvProducts.Columns.Contains("Id"))
            {
                dgvProducts.Columns["Id"].Visible = false;
            }
        }

        // تحديث واجهة السلة
        private void UpdateCartUI()
        {
            lvCart.Items.Clear();

            if (currentOrder != null && currentOrder.Items.Count > 0)
            {
                foreach (var item in currentOrder.Items)
                {
                    ListViewItem lvi = new ListViewItem(item.Product.Name);
                    lvi.SubItems.Add(item.Quantity.ToString());
                    lvi.SubItems.Add($"${item.Product.Price:F2}");
                    lvi.SubItems.Add($"${item.SubTotal:F2}");
                    lvCart.Items.Add(lvi);
                }

                lblTotal.Text = $"المجموع: ${currentOrder.TotalPrice:F2}";
                lblCartCount.Text = $"عدد القطع: {currentOrder.Items.Sum(i => i.Quantity)}";
                btnCheckout.Enabled = true;
            }
            else
            {
                lblTotal.Text = "المجموع: $0.00";
                lblCartCount.Text = "عدد القطع: 0";
                btnCheckout.Enabled = false;
            }
        }

        // إضافة منتج إلى السلة
        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            // التحقق من اختيار عميل
            if (cmbCustomers.SelectedItem == null)
            {
                MessageBox.Show("الرجاء اختيار عميل أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // التحقق من اختيار منتج
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار منتج أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // جلب المنتج المختار
            string productId = dgvProducts.SelectedRows[0].Cells["Id"].Value.ToString();
            Product product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                MessageBox.Show("المنتج غير موجود.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // إنشاء طلب جديد إذا لم يكن موجوداً أو إذا كان العميل مختلفاً
            Customer selectedCustomer = (Customer)cmbCustomers.SelectedItem;

            if (currentOrder == null || currentOrder.Customer != selectedCustomer)
            {
                currentOrder = new Order(Guid.NewGuid().ToString(), selectedCustomer);
            }

            // إضافة المنتج إلى الطلب
            try
            {
                int quantity = (int)nudQuantity.Value;
                currentOrder.AddItem(product, quantity);

                UpdateCartUI();
                LoadProducts(); // تحديث الكمية الظاهرة في الجدول

                lblStatus.Text = $"✓ تم إضافة {product.Name} إلى السلة";
                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

                MessageBox.Show($"تم إضافة {quantity} × {product.Name} إلى السلة!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = $"✗ فشل إضافة المنتج: {ex.Message}";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
            }
        }

        // حذف منتج من السلة
        private void BtnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (currentOrder == null || lvCart.SelectedItems.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار منتج للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = lvCart.SelectedItems[0].Text;
            var item = currentOrder.Items.FirstOrDefault(i => i.Product.Name == productName);

            if (item != null)
            {
                currentOrder.RemoveItem(item.Product.Id);
                UpdateCartUI();
                LoadProducts();

                lblStatus.Text = $"✓ تم حذف {productName} من السلة";
                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        // إتمام الشراء
        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (currentOrder == null || currentOrder.Items.Count == 0)
            {
                MessageBox.Show("السلة فارغة. الرجاء إضافة منتجات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // فتح نافذة الدفع
            PaymentForm paymentForm = new PaymentForm(currentOrder.TotalPrice);

            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // تعيين طريقة الدفع ومعالجة الطلب
                    currentOrder.SetPayment(paymentForm.SelectedPayment);
                    currentOrder.ProcessOrder();

                    // إضافة الطلب إلى قائمة طلبات العميل
                    currentOrder.Customer.Orders.Add(currentOrder);

                    // عرض ملخص الطلب
                    MessageBox.Show(currentOrder.GetOrderSummary(), "تم تأكيد الطلب", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // إعادة تعيين السلة لطلب جديد
                    currentOrder = null;
                    UpdateCartUI();

                    lblStatus.Text = "✓ تم إتمام الطلب بنجاح! شكراً لك";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // فتح نافذة إدارة العملاء
        private void BtnManageCustomers_Click(object sender, EventArgs e)
        {
            CustomersForm customersForm = new CustomersForm(customers);
            customersForm.ShowDialog();
            LoadCustomers();
        }

        // فتح نافذة إدارة المنتجات
        private void BtnManageProducts_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm(products);
            productsForm.ShowDialog();
            LoadProducts();
        }
    }
}