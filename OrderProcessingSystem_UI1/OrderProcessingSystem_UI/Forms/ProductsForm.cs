using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OrderProcessingSystem_UI.Models;

namespace OrderProcessingSystem_UI.Forms
{
    public class ProductsForm : Form
    {
        // القائمة اللي حتخزن المنتجات (تجي من البرنامج الرئيسي)
        private List<Product> products;

        // عناصر الواجهة
        private DataGridView dgvProducts;
        private ComboBox cmbCategory;
        private TextBox txtName, txtPrice, txtStock;
        private TextBox txtBrand, txtSize, txtColor;
        private NumericUpDown nudWarranty;
        private Button btnAdd, btnUpdate, btnDelete, btnRefresh;

        // المنشئ - يستقبل المنتجات من البرنامج الرئيسي
        public ProductsForm(List<Product> productList)
        {
            products = productList;
            InitializeMyForm();
            LoadProducts();
        }

        // تصميم الواجهة كاملة
        private void InitializeMyForm()
        {
            // إعدادات النافذة
            this.Text = "📦 إدارة المنتجات";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // ======== اللوحة العلوية (الرأس) ========
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);

            Label lblTitle = new Label();
            lblTitle.Text = "📦 إدارة المنتجات";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(20, 12);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            // ======== اللوحة الرئيسية ========
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(20);
            mainPanel.AutoScroll = true;

            // ======== اختيار نوع المنتج ========
            GroupBox categoryGroup = new GroupBox();
            categoryGroup.Text = "📂 نوع المنتج";
            categoryGroup.Location = new Point(0, 0);
            categoryGroup.Size = new Size(400, 65);
            categoryGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            categoryGroup.BackColor = Color.White;

            cmbCategory = new ComboBox();
            cmbCategory.Location = new Point(15, 25);
            cmbCategory.Size = new Size(200, 30);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new object[] { "Electronics", "Clothing" });
            cmbCategory.SelectedIndex = 0;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;

            categoryGroup.Controls.Add(cmbCategory);

            // ======== المعلومات الأساسية ========
            GroupBox basicGroup = new GroupBox();
            basicGroup.Text = "📝 المعلومات الأساسية";
            basicGroup.Location = new Point(0, 75);
            basicGroup.Size = new Size(400, 150);
            basicGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            basicGroup.BackColor = Color.White;

            // حقل الاسم
            Label lblName = new Label();
            lblName.Text = "اسم المنتج:";
            lblName.Location = new Point(15, 30);
            lblName.Size = new Size(100, 25);
            txtName = new TextBox();
            txtName.Location = new Point(120, 28);
            txtName.Size = new Size(250, 25);

            // حقل السعر
            Label lblPrice = new Label();
            lblPrice.Text = "السعر ($):";
            lblPrice.Location = new Point(15, 65);
            lblPrice.Size = new Size(100, 25);
            txtPrice = new TextBox();
            txtPrice.Location = new Point(120, 63);
            txtPrice.Size = new Size(250, 25);

            // حقل المخزون
            Label lblStock = new Label();
            lblStock.Text = "الكمية بالمخزون:";
            lblStock.Location = new Point(15, 100);
            lblStock.Size = new Size(100, 25);
            txtStock = new TextBox();
            txtStock.Location = new Point(120, 98);
            txtStock.Size = new Size(250, 25);

            basicGroup.Controls.Add(lblName);
            basicGroup.Controls.Add(txtName);
            basicGroup.Controls.Add(lblPrice);
            basicGroup.Controls.Add(txtPrice);
            basicGroup.Controls.Add(lblStock);
            basicGroup.Controls.Add(txtStock);

            // ======== التفاصيل الخاصة حسب النوع ========
            GroupBox specificGroup = new GroupBox();
            specificGroup.Text = "🔧 تفاصيل إضافية";
            specificGroup.Location = new Point(0, 235);
            specificGroup.Size = new Size(400, 150);
            specificGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            specificGroup.BackColor = Color.White;

            // حقول الإلكترونيات
            Label lblBrand = new Label();
            lblBrand.Text = "العلامة التجارية:";
            lblBrand.Location = new Point(15, 30);
            lblBrand.Size = new Size(120, 25);
            txtBrand = new TextBox();
            txtBrand.Location = new Point(140, 28);
            txtBrand.Size = new Size(230, 25);

            Label lblWarranty = new Label();
            lblWarranty.Text = "الضمان (شهر):";
            lblWarranty.Location = new Point(15, 65);
            lblWarranty.Size = new Size(120, 25);
            nudWarranty = new NumericUpDown();
            nudWarranty.Location = new Point(140, 63);
            nudWarranty.Size = new Size(100, 25);
            nudWarranty.Minimum = 0;
            nudWarranty.Maximum = 60;
            nudWarranty.Value = 12;

            // حقول الملابس
            Label lblSize = new Label();
            lblSize.Text = "المقاس:";
            lblSize.Location = new Point(15, 30);
            lblSize.Size = new Size(80, 25);
            txtSize = new TextBox();
            txtSize.Location = new Point(100, 28);
            txtSize.Size = new Size(80, 25);
            txtSize.Text = "M";

            Label lblColor = new Label();
            lblColor.Text = "اللون:";
            lblColor.Location = new Point(200, 30);
            lblColor.Size = new Size(60, 25);
            txtColor = new TextBox();
            txtColor.Location = new Point(260, 28);
            txtColor.Size = new Size(100, 25);
            txtColor.Text = "Black";

            specificGroup.Controls.Add(lblBrand);
            specificGroup.Controls.Add(txtBrand);
            specificGroup.Controls.Add(lblWarranty);
            specificGroup.Controls.Add(nudWarranty);
            specificGroup.Controls.Add(lblSize);
            specificGroup.Controls.Add(txtSize);
            specificGroup.Controls.Add(lblColor);
            specificGroup.Controls.Add(txtColor);

            // ======== الأزرار ========
            btnAdd = new Button();
            btnAdd.Text = "➕ إضافة";
            btnAdd.Location = new Point(0, 400);
            btnAdd.Size = new Size(125, 40);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = new Button();
            btnUpdate.Text = "✏️ تعديل";
            btnUpdate.Location = new Point(135, 400);
            btnUpdate.Size = new Size(125, 40);
            btnUpdate.BackColor = Color.FromArgb(52, 152, 219);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = new Button();
            btnDelete.Text = "🗑️ حذف";
            btnDelete.Location = new Point(270, 400);
            btnDelete.Size = new Size(125, 40);
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.Click += BtnDelete_Click;

            // ======== جدول المنتجات ========
            GroupBox listGroup = new GroupBox();
            listGroup.Text = "📋 قائمة المنتجات";
            listGroup.Location = new Point(420, 0);
            listGroup.Size = new Size(490, 520);
            listGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            listGroup.BackColor = Color.White;

            dgvProducts = new DataGridView();
            dgvProducts.Location = new Point(10, 25);
            dgvProducts.Size = new Size(465, 480);
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            listGroup.Controls.Add(dgvProducts);

            // ======== زر التحديث ========
            btnRefresh = new Button();
            btnRefresh.Text = "🔄 تحديث";
            btnRefresh.Location = new Point(0, 450);
            btnRefresh.Size = new Size(400, 35);
            btnRefresh.BackColor = Color.FromArgb(52, 73, 94);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += (s, e) => LoadProducts();

            // إضافة كل شيء إلى اللوحة الرئيسية
            mainPanel.Controls.Add(categoryGroup);
            mainPanel.Controls.Add(basicGroup);
            mainPanel.Controls.Add(specificGroup);
            mainPanel.Controls.Add(btnAdd);
            mainPanel.Controls.Add(btnUpdate);
            mainPanel.Controls.Add(btnDelete);
            mainPanel.Controls.Add(btnRefresh);
            mainPanel.Controls.Add(listGroup);

            // إضافة اللوحات إلى النافذة
            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);

            // إظهار/إخفاء الحقول حسب نوع المنتج
            UpdateSpecificFieldsVisibility();
        }

        // إظهار/إخفاء الحقول حسب نوع المنتج
        private void UpdateSpecificFieldsVisibility()
        {
            bool isElectronics = cmbCategory.SelectedItem?.ToString() == "Electronics";

            // حقول الإلكترونيات
            txtBrand.Visible = isElectronics;
            nudWarranty.Visible = isElectronics;

            // حقول الملابس
            txtSize.Visible = !isElectronics;
            txtColor.Visible = !isElectronics;
        }

        // عند تغيير نوع المنتج
        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSpecificFieldsVisibility();
        }

        // تحميل المنتجات في الجدول
        private void LoadProducts()
        {
            var productList = products.Select(p => new
            {
                p.Id,
                p.Name,
                Price = p.Price.ToString("F2"),
                Category = p.GetCategory(),
                p.StockQuantity,
                Discount = $"{p.CalculateDiscount():F2}",
                Details = p is ElectronicsProduct ep ? $"{ep.Brand} ({ep.WarrantyMonths}m)" :
                          p is ClothingProduct cp ? $"{cp.Size}/{cp.Color}" : ""
            }).ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = productList;

            // إخفاء عمود Id
            if (dgvProducts.Columns.Contains("Id"))
                dgvProducts.Columns["Id"].Visible = false;
        }

        // تفريغ الحقول
        private void ClearFields()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtBrand.Clear();
            nudWarranty.Value = 12;
            txtSize.Text = "M";
            txtColor.Text = "Black";
        }

        // إضافة منتج جديد
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم المنتج.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtPrice.Text, out double price))
            {
                MessageBox.Show("الرجاء إدخال سعر صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("الرجاء إدخال كمية صحيحة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product newProduct;

            if (cmbCategory.SelectedItem.ToString() == "Electronics")
            {
                newProduct = new ElectronicsProduct(
                    Guid.NewGuid().ToString(),
                    txtName.Text.Trim(),
                    price,
                    stock,
                    txtBrand.Text.Trim(),
                    (int)nudWarranty.Value
                );
            }
            else
            {
                newProduct = new ClothingProduct(
                    Guid.NewGuid().ToString(),
                    txtName.Text.Trim(),
                    price,
                    stock,
                    txtSize.Text.Trim(),
                    txtColor.Text.Trim()
                );
            }

            products.Add(newProduct);
            LoadProducts();
            ClearFields();
            MessageBox.Show("✅ تم إضافة المنتج بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // تعديل منتج موجود
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار منتج للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productId = dgvProducts.SelectedRows[0].Cells["Id"].Value.ToString();
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                product.Name = txtName.Text.Trim();
                product.Price = double.Parse(txtPrice.Text);
                product.StockQuantity = int.Parse(txtStock.Text);

                if (product is ElectronicsProduct ep)
                {
                    ep.Brand = txtBrand.Text.Trim();
                    ep.WarrantyMonths = (int)nudWarranty.Value;
                }
                else if (product is ClothingProduct cp)
                {
                    cp.Size = txtSize.Text.Trim();
                    cp.Color = txtColor.Text.Trim();
                }

                LoadProducts();
                ClearFields();
                MessageBox.Show("✅ تم تعديل المنتج بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // حذف منتج
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار منتج للحذف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("هل أنت متأكد من حذف هذا المنتج؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string productId = dgvProducts.SelectedRows[0].Cells["Id"].Value.ToString();
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    products.Remove(product);
                    LoadProducts();
                    ClearFields();
                    MessageBox.Show("✅ تم حذف المنتج بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // عند اختيار منتج من الجدول
        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                string productId = dgvProducts.SelectedRows[0].Cells["Id"].Value.ToString();
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    txtName.Text = product.Name;
                    txtPrice.Text = product.Price.ToString();
                    txtStock.Text = product.StockQuantity.ToString();

                    if (product is ElectronicsProduct ep)
                    {
                        cmbCategory.SelectedItem = "Electronics";
                        txtBrand.Text = ep.Brand;
                        nudWarranty.Value = ep.WarrantyMonths;
                    }
                    else if (product is ClothingProduct cp)
                    {
                        cmbCategory.SelectedItem = "Clothing";
                        txtSize.Text = cp.Size;
                        txtColor.Text = cp.Color;
                    }

                    UpdateSpecificFieldsVisibility();
                }
            }
        }
    }
}