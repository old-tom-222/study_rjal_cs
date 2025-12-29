using System;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public class ProductForm : Form
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;
        public Product NewProduct { get; private set; }
        public Product EditingProduct { get; private set; }
        private bool _isEditing = false;

        // 控件声明
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtSku;
        private TextBox txtProductName;
        private ComboBox cboCategory;
        private TextBox txtCostPrice;
        private TextBox txtSalePrice;
        private TextBox txtSafeStock;
        private TextBox txtStatus;
        private DateTimePicker dtpCreatedDate;
        private DateTimePicker dtpLastUpdated;
        private Button btnSave;
        private Button btnCancel;

        public ProductForm()
        {
            _productService = new ProductService();
            _categoryService = new ProductCategoryService();
            InitializeComponent();
            InitializeControls();
        }

        public ProductForm(Product product)
        {
            _productService = new ProductService();
            _categoryService = new ProductCategoryService();
            EditingProduct = product;
            _isEditing = true;
            InitializeComponent();
            InitializeControls();
            LoadProductData();
        }

        private void InitializeControls()
        {
            // 设置默认值
            txtStatus.Text = "1";
            dtpCreatedDate.Value = DateTime.Now;
            dtpLastUpdated.Value = DateTime.Now;
            dtpCreatedDate.Enabled = false;
            dtpLastUpdated.Enabled = false;

            // 加载分类下拉框
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAllProductCategories();
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载分类失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            if (EditingProduct != null)
            {
                this.Text = "编辑商品";
                txtSku.Text = EditingProduct.Sku;
                txtProductName.Text = EditingProduct.ProductName;
                txtCostPrice.Text = EditingProduct.CostPrice.ToString();
                txtSalePrice.Text = EditingProduct.SalePrice.ToString();
                txtSafeStock.Text = EditingProduct.SafeStock.ToString();
                txtStatus.Text = EditingProduct.Status;
                dtpCreatedDate.Value = EditingProduct.CreatedDate;
                dtpLastUpdated.Value = DateTime.Now;
                
                // 设置分类
                cboCategory.SelectedValue = EditingProduct.CategoryId;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(txtSku.Text))
            {
                MessageBox.Show("请输入商品SKU！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSku.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("请输入商品名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProductName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCostPrice.Text) || !decimal.TryParse(txtCostPrice.Text, out decimal costPrice))
            {
                MessageBox.Show("请输入有效的成本价！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCostPrice.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSalePrice.Text) || !decimal.TryParse(txtSalePrice.Text, out decimal salePrice))
            {
                MessageBox.Show("请输入有效的售价！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSalePrice.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSafeStock.Text) || !int.TryParse(txtSafeStock.Text, out int safeStock))
            {
                MessageBox.Show("请输入有效的安全库存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSafeStock.Focus();
                return;
            }

            try
            {
                if (_isEditing)
                {
                    // 更新现有商品
                    EditingProduct.Sku = txtSku.Text.Trim();
                    EditingProduct.ProductName = txtProductName.Text.Trim();
                    EditingProduct.CategoryId = (int)cboCategory.SelectedValue;
                    EditingProduct.CostPrice = costPrice;
                    EditingProduct.SalePrice = salePrice;
                    EditingProduct.SafeStock = safeStock;
                    EditingProduct.Status = txtStatus.Text.Trim();
                    EditingProduct.LastUpdated = DateTime.Now;

                    _productService.UpdateProduct(EditingProduct);
                    MessageBox.Show("商品更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建新商品
                    var product = new Product
                    {
                        Sku = txtSku.Text.Trim(),
                        ProductName = txtProductName.Text.Trim(),
                        CategoryId = (int)cboCategory.SelectedValue,
                        CostPrice = costPrice,
                        SalePrice = salePrice,
                        SafeStock = safeStock,
                        Status = txtStatus.Text.Trim(),
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now
                    };

                    int newProductId = _productService.CreateProduct(product);
                    product.ProductId = newProductId;
                    NewProduct = product;

                    MessageBox.Show("商品添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = _isEditing ? "更新商品失败: " : "添加商品失败: ";
                MessageBox.Show(errorMessage + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.txtSafeStock = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.dtpLastUpdated = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SKU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "商品名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "分类";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "成本价";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "售价";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "安全库存";
            // 
            // txtSku
            // 
            this.txtSku.Location = new System.Drawing.Point(120, 12);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(200, 20);
            this.txtSku.TabIndex = 6;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(120, 38);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(200, 20);
            this.txtProductName.TabIndex = 7;
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(120, 64);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(200, 21);
            this.cboCategory.TabIndex = 8;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Location = new System.Drawing.Point(120, 90);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(200, 20);
            this.txtCostPrice.TabIndex = 9;
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(120, 116);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(200, 20);
            this.txtSalePrice.TabIndex = 10;
            // 
            // txtSafeStock
            // 
            this.txtSafeStock.Location = new System.Drawing.Point(120, 142);
            this.txtSafeStock.Name = "txtSafeStock";
            this.txtSafeStock.Size = new System.Drawing.Size(200, 20);
            this.txtSafeStock.TabIndex = 11;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 168);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 20);
            this.txtStatus.TabIndex = 12;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(120, 194);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreatedDate.TabIndex = 13;
            // 
            // dtpLastUpdated
            // 
            this.dtpLastUpdated.Location = new System.Drawing.Point(120, 220);
            this.dtpLastUpdated.Name = "dtpLastUpdated";
            this.dtpLastUpdated.Size = new System.Drawing.Size(200, 20);
            this.dtpLastUpdated.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(241, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 291);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpLastUpdated);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtSafeStock);
            this.Controls.Add(this.txtSalePrice);
            this.Controls.Add(this.txtCostPrice);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.txtSku);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProductForm";
            this.Text = "添加商品";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}