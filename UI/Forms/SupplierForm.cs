using System;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public class SupplierForm : Form
    {
        private readonly SupplierService _supplierService;
        public Supplier NewSupplier { get; private set; }
        public Supplier EditingSupplier { get; private set; }
        private bool _isEditing = false;

        // 控件声明
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtSupplierName;
        private TextBox txtContactPerson;
        private TextBox txtPhone;
        private TextBox txtStatus;
        private DateTimePicker dtpCreatedDate;
        private DateTimePicker dtpLastUpdated;
        private Button btnSave;
        private Button btnCancel;

        public SupplierForm()
        {
            _supplierService = new SupplierService();
            InitializeComponent();
            InitializeControls();
        }

        public SupplierForm(Supplier supplier)
        {
            _supplierService = new SupplierService();
            EditingSupplier = supplier;
            _isEditing = true;
            InitializeComponent();
            InitializeControls();
            LoadSupplierData();
        }

        private void InitializeControls()
        {
            // 设置默认值
            txtStatus.Text = "1";
            dtpCreatedDate.Value = DateTime.Now;
            dtpLastUpdated.Value = DateTime.Now;
            dtpCreatedDate.Enabled = false;
            dtpLastUpdated.Enabled = false;
        }

        private void LoadSupplierData()
        {
            if (EditingSupplier != null)
            {
                this.Text = "编辑供应商";
                txtSupplierName.Text = EditingSupplier.SupplierName;
                txtContactPerson.Text = EditingSupplier.ContactPerson;
                txtPhone.Text = EditingSupplier.Phone;
                txtStatus.Text = EditingSupplier.Status;
                dtpCreatedDate.Value = EditingSupplier.CreatedDate;
                dtpLastUpdated.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("请输入供应商名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupplierName.Focus();
                return;
            }

            try
            {
                if (_isEditing)
                {
                    // 更新现有供应商
                    EditingSupplier.SupplierName = txtSupplierName.Text.Trim();
                    EditingSupplier.ContactPerson = txtContactPerson.Text.Trim();
                    EditingSupplier.Phone = txtPhone.Text.Trim();
                    EditingSupplier.Status = txtStatus.Text.Trim();
                    EditingSupplier.LastUpdated = DateTime.Now;

                    _supplierService.UpdateSupplier(EditingSupplier);
                    MessageBox.Show("供应商更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建新供应商
                    var supplier = new Supplier
                    {
                        SupplierName = txtSupplierName.Text.Trim(),
                        ContactPerson = txtContactPerson.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Status = txtStatus.Text.Trim(),
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now
                    };

                    int newSupplierId = _supplierService.CreateSupplier(supplier);
                    supplier.SupplierId = newSupplierId;
                    NewSupplier = supplier;

                    MessageBox.Show("供应商添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = _isEditing ? "更新供应商失败: " : "添加供应商失败: ";
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
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
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
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "供应商名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "联系人";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "电话";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(120, 12);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(200, 20);
            this.txtSupplierName.TabIndex = 3;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(120, 38);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(200, 20);
            this.txtContactPerson.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(120, 64);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 5;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 90);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 20);
            this.txtStatus.TabIndex = 6;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(120, 116);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreatedDate.TabIndex = 7;
            // 
            // dtpLastUpdated
            // 
            this.dtpLastUpdated.Location = new System.Drawing.Point(120, 142);
            this.dtpLastUpdated.Name = "dtpLastUpdated";
            this.dtpLastUpdated.Size = new System.Drawing.Size(200, 20);
            this.dtpLastUpdated.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(241, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 211);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpLastUpdated);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtSupplierName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SupplierForm";
            this.Text = "添加供应商";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}