using System;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public class ProductCategoryForm : Form
    {
        private readonly ProductCategoryService _categoryService;
        public ProductCategory NewCategory { get; private set; }
        public ProductCategory EditingCategory { get; private set; }
        private bool _isEditing = false;

        // 控件声明
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtCategoryName;
        private ComboBox cboParentCategory;
        private TextBox txtStatus;
        private DateTimePicker dtpCreatedDate;
        private DateTimePicker dtpLastUpdated;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;

        public ProductCategoryForm()
        {
            _categoryService = new ProductCategoryService();
            InitializeComponent();
            InitializeControls();
        }

        public ProductCategoryForm(ProductCategory category)
        {
            _categoryService = new ProductCategoryService();
            EditingCategory = category;
            _isEditing = true;
            InitializeComponent();
            InitializeControls();
            LoadCategoryData();
        }

        private void InitializeControls()
        {
            // 设置默认值
            txtStatus.Text = "1";
            dtpCreatedDate.Value = DateTime.Now;
            dtpLastUpdated.Value = DateTime.Now;
            dtpCreatedDate.Enabled = false;
            dtpLastUpdated.Enabled = false;

            // 加载父分类下拉框
            LoadParentCategories();
        }

        private void LoadParentCategories()
        {
            try
            {
                var categories = _categoryService.GetAllProductCategories();
                cboParentCategory.DataSource = categories;
                cboParentCategory.DisplayMember = "CategoryName";
                cboParentCategory.ValueMember = "CategoryId";
                cboParentCategory.Items.Insert(0, new ProductCategory { CategoryId = 0, CategoryName = "无父分类" });
                cboParentCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载父分类失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryData()
        {
            if (EditingCategory != null)
            {
                this.Text = "编辑商品分类";
                txtCategoryName.Text = EditingCategory.CategoryName;
                txtStatus.Text = EditingCategory.Status;
                txtNotes.Text = EditingCategory.Notes;
                dtpCreatedDate.Value = EditingCategory.CreatedDate;
                dtpLastUpdated.Value = DateTime.Now;
                
                // 设置父分类
                if (EditingCategory.ParentCategoryId.HasValue)
                {
                    cboParentCategory.SelectedValue = EditingCategory.ParentCategoryId.Value;
                }
                else
                {
                    cboParentCategory.SelectedIndex = 0;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("请输入分类名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoryName.Focus();
                return;
            }

            try
            {
                if (_isEditing)
                {
                    // 更新现有分类
                    EditingCategory.CategoryName = txtCategoryName.Text.Trim();
                    EditingCategory.ParentCategoryId = cboParentCategory.SelectedValue.ToString() == "0" ? (int?)null : (int?)cboParentCategory.SelectedValue;
                    EditingCategory.Status = txtStatus.Text.Trim();
                    EditingCategory.Notes = txtNotes.Text.Trim();
                    EditingCategory.LastUpdated = DateTime.Now;

                    _categoryService.UpdateProductCategory(EditingCategory);
                    MessageBox.Show("商品分类更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建新分类
                    var category = new ProductCategory
                    {
                        CategoryName = txtCategoryName.Text.Trim(),
                        ParentCategoryId = cboParentCategory.SelectedValue.ToString() == "0" ? (int?)null : (int?)cboParentCategory.SelectedValue,
                        Status = txtStatus.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now
                    };

                    int newCategoryId = _categoryService.CreateProductCategory(category);
                    category.CategoryId = newCategoryId;
                    NewCategory = category;

                    MessageBox.Show("商品分类添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = _isEditing ? "更新商品分类失败: " : "添加商品分类失败: ";
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
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.cboParentCategory = new System.Windows.Forms.ComboBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.dtpLastUpdated = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "分类名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "父分类";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "状态";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(120, 12);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(200, 20);
            this.txtCategoryName.TabIndex = 4;
            // 
            // cboParentCategory
            // 
            this.cboParentCategory.FormattingEnabled = true;
            this.cboParentCategory.Location = new System.Drawing.Point(120, 38);
            this.cboParentCategory.Name = "cboParentCategory";
            this.cboParentCategory.Size = new System.Drawing.Size(200, 21);
            this.cboParentCategory.TabIndex = 5;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 64);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 20);
            this.txtStatus.TabIndex = 6;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(120, 90);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreatedDate.TabIndex = 7;
            // 
            // dtpLastUpdated
            // 
            this.dtpLastUpdated.Location = new System.Drawing.Point(120, 116);
            this.dtpLastUpdated.Name = "dtpLastUpdated";
            this.dtpLastUpdated.Size = new System.Drawing.Size(200, 20);
            this.dtpLastUpdated.TabIndex = 8;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(120, 142);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(200, 60);
            this.txtNotes.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(241, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProductCategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.dtpLastUpdated);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.cboParentCategory);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProductCategoryForm";
            this.Text = "添加商品分类";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}