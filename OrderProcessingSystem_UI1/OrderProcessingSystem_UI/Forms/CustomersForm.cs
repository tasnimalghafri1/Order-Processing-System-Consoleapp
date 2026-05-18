using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OrderProcessingSystem_UI.Models;

namespace OrderProcessingSystem_UI.Forms
{
    public partial class CustomersForm : Form
    {
        // القائمة اللي حتخزن العملاء (تجي من البرنامج الرئيسي)
        private List<Customer> customers;

        // عناصر الواجهة
        private DataGridView dgvCustomers;
        private TextBox txtName, txtEmail, txtPhone;
        private Button btnAdd, btnUpdate, btnDelete, btnRefresh;

        // المنشئ - يستقبل العملاء من البرنامج الرئيسي
        public CustomersForm(List<Customer> customerList)
        {
            customers = customerList;
            InitializeMyForm();  // تصميم الواجهة
            LoadCustomers();     // تحميل العملاء في الجدول
        }

        // هنا تصميم الواجهة كاملة
        private void InitializeMyForm()
        {
            // إعدادات النافذة
            this.Text = "👥 إدارة العملاء";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // ======== اللوحة العلوية (الرأس) ========
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);

            Label lblTitle = new Label();
            lblTitle.Text = "👥 إدارة العملاء";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(20, 12);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            // ======== اللوحة الرئيسية ========
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);

            // ======== مجموعة إدخال البيانات (جهة اليسار) ========
            GroupBox inputGroup = new GroupBox();
            inputGroup.Text = "📝 بيانات العميل";
            inputGroup.Location = new Point(0, 0);
            inputGroup.Size = new Size(360, 200);
            inputGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            inputGroup.BackColor = Color.White;

            // حقل الاسم
            Label lblName = new Label();
            lblName.Text = "الاسم كاملاً:";
            lblName.Location = new Point(15, 35);
            lblName.Size = new Size(100, 25);

            txtName = new TextBox();
            txtName.Location = new Point(120, 33);
            txtName.Size = new Size(210, 25);

            // حقل البريد الإلكتروني
            Label lblEmail = new Label();
            lblEmail.Text = "البريد الإلكتروني:";
            lblEmail.Location = new Point(15, 70);
            lblEmail.Size = new Size(100, 25);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(120, 68);
            txtEmail.Size = new Size(210, 25);

            // حقل الجوال
            Label lblPhone = new Label();
            lblPhone.Text = "الجوال:";
            lblPhone.Location = new Point(15, 105);
            lblPhone.Size = new Size(100, 25);

            txtPhone = new TextBox();
            txtPhone.Location = new Point(120, 103);
            txtPhone.Size = new Size(210, 25);

            // زر الإضافة
            btnAdd = new Button();
            btnAdd.Text = "➕ إضافة";
            btnAdd.Location = new Point(15, 145);
            btnAdd.Size = new Size(90, 35);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Click += BtnAdd_Click;  // ربط الحدث

            // زر التعديل
            btnUpdate = new Button();
            btnUpdate.Text = "✏️ تعديل";
            btnUpdate.Location = new Point(115, 145);
            btnUpdate.Size = new Size(90, 35);
            btnUpdate.BackColor = Color.FromArgb(52, 152, 219);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Click += BtnUpdate_Click;

            // زر الحذف
            btnDelete = new Button();
            btnDelete.Text = "🗑️ حذف";
            btnDelete.Location = new Point(215, 145);
            btnDelete.Size = new Size(90, 35);
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Click += BtnDelete_Click;

            // إضافة كل العناصر إلى مجموعة الإدخال
            inputGroup.Controls.Add(lblName);
            inputGroup.Controls.Add(txtName);
            inputGroup.Controls.Add(lblEmail);
            inputGroup.Controls.Add(txtEmail);
            inputGroup.Controls.Add(lblPhone);
            inputGroup.Controls.Add(txtPhone);
            inputGroup.Controls.Add(btnAdd);
            inputGroup.Controls.Add(btnUpdate);
            inputGroup.Controls.Add(btnDelete);

            // ======== مجموعة الجدول (جهة اليمين) ========
            GroupBox listGroup = new GroupBox();
            listGroup.Text = "📋 قائمة العملاء";
            listGroup.Location = new Point(380, 0);
            listGroup.Size = new Size(380, 460);
            listGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            listGroup.BackColor = Color.White;

            dgvCustomers = new DataGridView();
            dgvCustomers.Location = new Point(10, 25);
            dgvCustomers.Size = new Size(355, 420);
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;

            listGroup.Controls.Add(dgvCustomers);

            // ======== زر التحديث ========
            btnRefresh = new Button();
            btnRefresh.Text = "🔄 تحديث";
            btnRefresh.Location = new Point(0, 470);
            btnRefresh.Size = new Size(745, 35);
            btnRefresh.BackColor = Color.FromArgb(52, 73, 94);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += (s, e) => LoadCustomers();

            // إضافة كل المجموعات إلى اللوحة الرئيسية
            mainPanel.Controls.Add(inputGroup);
            mainPanel.Controls.Add(listGroup);
            mainPanel.Controls.Add(btnRefresh);

            // إضافة اللوحات إلى النافذة
            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
        }

        // تحميل العملاء في الجدول
        private void LoadCustomers()
        {
            var customerList = customers.Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                c.Phone,
                عدد_الطلبات = c.Orders?.Count ?? 0
            }).ToList();

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customerList;

            // إخفاء عمود Id عشان ما يظهر للمستخدم
            if (dgvCustomers.Columns.Contains("Id"))
                dgvCustomers.Columns["Id"].Visible = false;
        }

        // تفريغ الحقول
        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }

        // إضافة عميل جديد
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // التأكد من وجود اسم وإيميل
            if (txtName.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("الرجاء إدخال الاسم والبريد الإلكتروني", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // إنشاء عميل جديد
            Customer newCustomer = new Customer(
                Guid.NewGuid().ToString(),
                txtName.Text,
                txtEmail.Text,
                txtPhone.Text
            );

            // إضافة إلى القائمة
            customers.Add(newCustomer);

            // تحديث الجدول
            LoadCustomers();

            // تفريغ الحقول
            ClearFields();

            // رسالة نجاح
            MessageBox.Show("تم إضافة العميل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // تعديل عميل
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // التأكد من وجود عميل محدد
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار عميل للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // جلب ID العميل المحدد
            string id = dgvCustomers.SelectedRows[0].Cells["Id"].Value.ToString();

            // البحث عن العميل في القائمة
            Customer customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                // تعديل البيانات
                customer.Name = txtName.Text;
                customer.Email = txtEmail.Text;
                customer.Phone = txtPhone.Text;

                // تحديث الجدول
                LoadCustomers();

                // تفريغ الحقول
                ClearFields();

                MessageBox.Show("تم تعديل العميل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // حذف عميل
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // التأكد من وجود عميل محدد
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار عميل للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // طلب تأكيد
            DialogResult result = MessageBox.Show("هل أنت متأكد من الحذف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // جلب ID العميل
                string id = dgvCustomers.SelectedRows[0].Cells["Id"].Value.ToString();

                // البحث عن العميل
                Customer customer = customers.FirstOrDefault(c => c.Id == id);

                if (customer != null)
                {
                    // حذف العميل
                    customers.Remove(customer);

                    // تحديث الجدول
                    LoadCustomers();

                    // تفريغ الحقول
                    ClearFields();

                    MessageBox.Show("تم حذف العميل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // عند اختيار عميل من الجدول، يملأ الحقول
        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                string id = dgvCustomers.SelectedRows[0].Cells["Id"].Value.ToString();
                Customer customer = customers.FirstOrDefault(c => c.Id == id);

                if (customer != null)
                {
                    txtName.Text = customer.Name;
                    txtEmail.Text = customer.Email;
                    txtPhone.Text = customer.Phone;
                }
            }
        }
    }
}