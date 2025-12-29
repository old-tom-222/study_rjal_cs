using System;
using System.Drawing;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class MasterDataForm : Form
    {
        private readonly ProductService _productService = new ProductService();
        private readonly ProductCategoryService _categoryService = new ProductCategoryService();
        private readonly SupplierService _supplierService = new SupplierService();
        private readonly CustomerService _customerService = new CustomerService();
        private readonly WarehouseService _warehouseService = new WarehouseService();
        private TabControl tabControl1;

        public MasterDataForm()
        {
            // 初始化基本窗体设置 - 与库存管理模块保持一致
            this.Size = new System.Drawing.Size(900, 600);
            this.Text = "基础数据管理";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // 创建tabControl1
            tabControl1 = new TabControl();
            tabControl1.Dock = DockStyle.Fill;
            this.Controls.Add(tabControl1);
            
            this.Load += MasterDataForm_Load;
        }

        private void MasterDataForm_Load(object sender, EventArgs e)
        {
            // 设置选项卡
            tabControl1.TabPages.Add("商品管理");
            tabControl1.TabPages.Add("商品分类管理");
            tabControl1.TabPages.Add("供应商管理");
            tabControl1.TabPages.Add("客户管理");
            tabControl1.TabPages.Add("仓库管理");

            // 初始化各个子界面
            InitializeProductTab();
            InitializeCategoryTab();
            InitializeSupplierTab();
            InitializeCustomerTab();
            InitializeWarehouseTab();

            // 加载所有标签页的数据
            LoadProducts();
            LoadCategories();
            LoadSuppliers();
            LoadCustomers();
            LoadWarehouses();
        }

        #region 商品管理
        private DataGridView gridProducts;
        private TextBox txtProductName;
        private ComboBox cboProductCategory;
        private Button btnProductQuery;
        private Button btnAddProduct;
        private Button btnEditProduct;

        private void InitializeProductTab()
        {
            var tabPage = tabControl1.TabPages[0];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "商品名称:", Location = new Point(20, 32) });
            txtProductName = new TextBox { Location = new Point(90, 30), Width = 150 };
            panelQuery.Controls.Add(txtProductName);

            panelQuery.Controls.Add(new Label { Text = "分类:", Location = new Point(260, 32) });
            cboProductCategory = new ComboBox { Location = new Point(310, 30), Width = 150 };
            panelQuery.Controls.Add(cboProductCategory);

            btnProductQuery = new Button { Text = "查询", Location = new Point(480, 30), Size = new Size(75, 23) };
            btnProductQuery.Click += BtnProductQuery_Click;
            panelQuery.Controls.Add(btnProductQuery);

            btnAddProduct = new Button { Text = "新增", Location = new Point(570, 30), Size = new Size(75, 23) };
            btnAddProduct.Click += BtnAddProduct_Click;
            panelQuery.Controls.Add(btnAddProduct);

            btnEditProduct = new Button { Text = "编辑", Location = new Point(660, 30), Size = new Size(75, 23) };
            btnEditProduct.Click += BtnEditProduct_Click;
            panelQuery.Controls.Add(btnEditProduct);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridProducts = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridProducts);

            // 设置数据网格列
            SetupProductGrid();

            // 加载分类数据
            LoadProductCategories();
        }

        private void SetupProductGrid()
        {
            gridProducts.AutoGenerateColumns = false;
            gridProducts.Columns.Clear();
            gridProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "ID", Width = 50 });
            gridProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "商品名称", Width = 200 });
            gridProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoryId", HeaderText = "分类ID", Width = 80 });
            gridProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoryName", HeaderText = "分类名称", Width = 150 });
            gridProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态", Width = 80 });
            gridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridProducts.ScrollBars = ScrollBars.Both;
            
            // 设置字体为支持中文的字体
            gridProducts.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9);
            gridProducts.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9, System.Drawing.FontStyle.Bold);
        }

        private void LoadProducts()
        {
            var productName = txtProductName?.Text?.Trim();
            int? categoryId = null;
            if (cboProductCategory.SelectedIndex > 0)
            {
                categoryId = ((ProductCategory)cboProductCategory.SelectedItem).CategoryId;
            }
            
            var list = _productService.GetProducts(productName, categoryId);
            gridProducts.DataSource = list;
        }

        private void LoadProductCategories()
        {
            var categories = _categoryService.GetProductCategories();
            cboProductCategory.Items.Add(new ProductCategory { CategoryId = 0, CategoryName = "" });
            foreach (var category in categories)
            {
                cboProductCategory.Items.Add(category);
            }
            cboProductCategory.DisplayMember = "CategoryName";
            cboProductCategory.ValueMember = "CategoryId";
            cboProductCategory.SelectedIndex = 0;
        }

        private void BtnProductQuery_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            var productForm = new ProductForm();
            if (productForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            if (gridProducts.SelectedRows.Count > 0)
            {
                var product = (Product)gridProducts.SelectedRows[0].DataBoundItem;
                var productForm = new ProductForm(product);
                if (productForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的商品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 商品分类管理
        private DataGridView gridCategories;
        private TextBox txtCategoryName;
        private Button btnCategoryQuery;
        private Button btnAddCategory;
        private Button btnEditCategory;

        private void InitializeCategoryTab()
        {
            var tabPage = tabControl1.TabPages[1];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "分类名称:", Location = new Point(20, 32) });
            txtCategoryName = new TextBox { Location = new Point(90, 30), Width = 150 };
            panelQuery.Controls.Add(txtCategoryName);

            btnCategoryQuery = new Button { Text = "查询", Location = new Point(260, 30), Size = new Size(75, 23) };
            btnCategoryQuery.Click += BtnCategoryQuery_Click;
            panelQuery.Controls.Add(btnCategoryQuery);

            btnAddCategory = new Button { Text = "新增", Location = new Point(350, 30), Size = new Size(75, 23) };
            btnAddCategory.Click += BtnAddCategory_Click;
            panelQuery.Controls.Add(btnAddCategory);

            btnEditCategory = new Button { Text = "编辑", Location = new Point(440, 30), Size = new Size(75, 23) };
            btnEditCategory.Click += BtnEditCategory_Click;
            panelQuery.Controls.Add(btnEditCategory);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridCategories = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridCategories);

            // 设置数据网格列
            SetupCategoryGrid();
        }

        private void SetupCategoryGrid()
        {
            gridCategories.AutoGenerateColumns = false;
            gridCategories.Columns.Clear();
            gridCategories.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoryId", HeaderText = "ID", Width = 50 });
            gridCategories.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoryName", HeaderText = "分类名称", Width = 200 });
            gridCategories.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态", Width = 80 });
            gridCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCategories.ScrollBars = ScrollBars.Both;
            
            // 设置字体为支持中文的字体
            gridCategories.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9);
            gridCategories.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9, System.Drawing.FontStyle.Bold);
        }

        private void LoadCategories()
        {
            var categoryName = txtCategoryName?.Text?.Trim();
            var list = _categoryService.GetProductCategories(categoryName);
            gridCategories.DataSource = list;
        }

        private void BtnCategoryQuery_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            var categoryForm = new ProductCategoryForm();
            if (categoryForm.ShowDialog() == DialogResult.OK)
            {
                // 刷新分类列表
                LoadCategories();
                // 同时刷新商品查询的分类下拉框
                LoadProductCategories();
            }
        }

        private void BtnEditCategory_Click(object sender, EventArgs e)
        {
            if (gridCategories.SelectedRows.Count > 0)
            {
                var category = (ProductCategory)gridCategories.SelectedRows[0].DataBoundItem;
                var categoryForm = new ProductCategoryForm(category);
                if (categoryForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories();
                    LoadProductCategories();
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的分类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 供应商管理
        private DataGridView gridSuppliers;
        private TextBox txtSupplierName;
        private Button btnSupplierQuery;
        private Button btnAddSupplier;
        private Button btnEditSupplier;

        private void InitializeSupplierTab()
        {
            var tabPage = tabControl1.TabPages[2];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "供应商名称:", Location = new Point(20, 32) });
            txtSupplierName = new TextBox { Location = new Point(100, 30), Width = 150 };
            panelQuery.Controls.Add(txtSupplierName);

            btnSupplierQuery = new Button { Text = "查询", Location = new Point(270, 30), Size = new Size(75, 23) };
            btnSupplierQuery.Click += BtnSupplierQuery_Click;
            panelQuery.Controls.Add(btnSupplierQuery);

            btnAddSupplier = new Button { Text = "新增", Location = new Point(360, 30), Size = new Size(75, 23) };
            btnAddSupplier.Click += BtnAddSupplier_Click;
            panelQuery.Controls.Add(btnAddSupplier);

            btnEditSupplier = new Button { Text = "编辑", Location = new Point(450, 30), Size = new Size(75, 23) };
            btnEditSupplier.Click += BtnEditSupplier_Click;
            panelQuery.Controls.Add(btnEditSupplier);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridSuppliers = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridSuppliers);

            // 设置数据网格列
            SetupSupplierGrid();
        }

        private void SetupSupplierGrid()
        {
            gridSuppliers.AutoGenerateColumns = false;
            gridSuppliers.Columns.Clear();
            gridSuppliers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SupplierId", HeaderText = "ID", Width = 50 });
            gridSuppliers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SupplierName", HeaderText = "供应商名称", Width = 200 });
            gridSuppliers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ContactPerson", HeaderText = "联系人", Width = 120 });
            gridSuppliers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Phone", HeaderText = "电话", Width = 150 });
            gridSuppliers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态", Width = 80 });
            gridSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSuppliers.ScrollBars = ScrollBars.Both;
            
            // 设置字体为支持中文的字体
            gridSuppliers.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9);
            gridSuppliers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9, System.Drawing.FontStyle.Bold);
        }

        private void LoadSuppliers()
        {
            var supplierName = txtSupplierName?.Text?.Trim();
            var list = _supplierService.GetSuppliers(supplierName);
            gridSuppliers.DataSource = list;
        }

        private void BtnSupplierQuery_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void BtnAddSupplier_Click(object sender, EventArgs e)
        {
            var supplierForm = new SupplierForm();
            if (supplierForm.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers();
            }
        }

        private void BtnEditSupplier_Click(object sender, EventArgs e)
        {
            if (gridSuppliers.SelectedRows.Count > 0)
            {
                var supplier = (Supplier)gridSuppliers.SelectedRows[0].DataBoundItem;
                var supplierForm = new SupplierForm(supplier);
                if (supplierForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的供应商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 客户管理
        private DataGridView gridCustomers;
        private TextBox txtCustomerName;
        private Button btnCustomerQuery;
        private Button btnAddCustomer;
        private Button btnEditCustomer;

        private void InitializeCustomerTab()
        {
            var tabPage = tabControl1.TabPages[3];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "客户名称:", Location = new Point(20, 32) });
            txtCustomerName = new TextBox { Location = new Point(100, 30), Width = 150 };
            panelQuery.Controls.Add(txtCustomerName);

            btnCustomerQuery = new Button { Text = "查询", Location = new Point(270, 30), Size = new Size(75, 23) };
            btnCustomerQuery.Click += BtnCustomerQuery_Click;
            panelQuery.Controls.Add(btnCustomerQuery);

            btnAddCustomer = new Button { Text = "新增", Location = new Point(360, 30), Size = new Size(75, 23) };
            btnAddCustomer.Click += BtnAddCustomer_Click;
            panelQuery.Controls.Add(btnAddCustomer);

            btnEditCustomer = new Button { Text = "编辑", Location = new Point(450, 30), Size = new Size(75, 23) };
            btnEditCustomer.Click += BtnEditCustomer_Click;
            panelQuery.Controls.Add(btnEditCustomer);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridCustomers = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridCustomers);

            // 设置数据网格列
            SetupCustomerGrid();
        }

        private void SetupCustomerGrid()
        {
            gridCustomers.AutoGenerateColumns = false;
            gridCustomers.Columns.Clear();
            gridCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerId", HeaderText = "ID", Width = 50 });
            gridCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "客户名称", Width = 200 });
            gridCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ContactPerson", HeaderText = "联系人", Width = 120 });
            gridCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ContactPhone", HeaderText = "电话", Width = 150 });
            
            // 添加状态列，使用事件处理数字到中文的转换
            var statusColumn = new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态", Width = 80 };
            gridCustomers.Columns.Add(statusColumn);
            
            // 为状态列添加CellFormatting事件处理
            gridCustomers.CellFormatting += (sender, e) =>
            {
                // 确保所有对象都不为null，避免空引用异常
                if (e != null && gridCustomers != null && gridCustomers.Columns != null && gridCustomers.Columns.Contains("Status"))
                {
                    DataGridViewColumn statusCol = gridCustomers.Columns["Status"];
                    if (statusCol != null && e.ColumnIndex == statusCol.Index && e.Value != null)
                    {
                        string statusValue = e.Value.ToString();
                        if (statusValue == "1")
                            e.Value = "活跃";
                        else if (statusValue == "0")
                            e.Value = "禁用";
                        e.FormattingApplied = true;
                    }
                }
            };
            
            gridCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCustomers.ScrollBars = ScrollBars.Both;
            
            // 设置字体为支持中文的字体
            gridCustomers.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9);
            gridCustomers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9, System.Drawing.FontStyle.Bold);
        }

        private void LoadCustomers()
        {
            var customerName = txtCustomerName?.Text?.Trim();
            var list = _customerService.GetCustomers(customerName);
            gridCustomers.DataSource = list;
        }

        private void BtnCustomerQuery_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            var customerForm = new CustomerForm();
            if (customerForm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers();
            }
        }

        private void BtnEditCustomer_Click(object sender, EventArgs e)
        {
            if (gridCustomers.SelectedRows.Count > 0)
            {
                var customer = (Customer)gridCustomers.SelectedRows[0].DataBoundItem;
                var customerForm = new CustomerForm(customer);
                if (customerForm.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的客户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 仓库管理
        private DataGridView gridWarehouses;
        private TextBox txtWarehouseName;
        private Button btnWarehouseQuery;
        private Button btnAddWarehouse;
        private Button btnEditWarehouse;

        private void InitializeWarehouseTab()
        {
            var tabPage = tabControl1.TabPages[4];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "仓库名称:", Location = new Point(20, 32) });
            txtWarehouseName = new TextBox { Location = new Point(100, 30), Width = 150 };
            panelQuery.Controls.Add(txtWarehouseName);

            btnWarehouseQuery = new Button { Text = "查询", Location = new Point(270, 30), Size = new Size(75, 23) };
            btnWarehouseQuery.Click += BtnWarehouseQuery_Click;
            panelQuery.Controls.Add(btnWarehouseQuery);

            btnAddWarehouse = new Button { Text = "新增", Location = new Point(360, 30), Size = new Size(75, 23) };
            btnAddWarehouse.Click += BtnAddWarehouse_Click;
            panelQuery.Controls.Add(btnAddWarehouse);

            btnEditWarehouse = new Button { Text = "编辑", Location = new Point(450, 30), Size = new Size(75, 23) };
            btnEditWarehouse.Click += BtnEditWarehouse_Click;
            panelQuery.Controls.Add(btnEditWarehouse);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridWarehouses = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridWarehouses);

            // 设置数据网格列
            SetupWarehouseGrid();
        }

        private void SetupWarehouseGrid()
        {
            gridWarehouses.AutoGenerateColumns = false;
            gridWarehouses.Columns.Clear();
            gridWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseId", HeaderText = "ID", Width = 50 });
            gridWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseName", HeaderText = "仓库名称", Width = 200 });
            gridWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Address", HeaderText = "地址", Width = 300 });
            gridWarehouses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态", Width = 80 });
            gridWarehouses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridWarehouses.ScrollBars = ScrollBars.Both;
            
            // 设置字体为支持中文的字体
            gridWarehouses.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9);
            gridWarehouses.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei", 9, System.Drawing.FontStyle.Bold);
        }

        private void LoadWarehouses()
        {
            var warehouseName = txtWarehouseName?.Text?.Trim();
            var list = _warehouseService.GetWarehouses(warehouseName);
            gridWarehouses.DataSource = list;
        }

        private void BtnWarehouseQuery_Click(object sender, EventArgs e)
        {
            LoadWarehouses();
        }

        private void BtnAddWarehouse_Click(object sender, EventArgs e)
        {
            var warehouseForm = new WarehouseForm();
            if (warehouseForm.ShowDialog() == DialogResult.OK)
            {
                LoadWarehouses();
            }
        }

        private void BtnEditWarehouse_Click(object sender, EventArgs e)
        {
            if (gridWarehouses.SelectedRows.Count > 0)
            {
                var warehouse = (Warehouse)gridWarehouses.SelectedRows[0].DataBoundItem;
                var warehouseForm = new WarehouseForm(warehouse);
                if (warehouseForm.ShowDialog() == DialogResult.OK)
                {
                    LoadWarehouses();
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的仓库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}