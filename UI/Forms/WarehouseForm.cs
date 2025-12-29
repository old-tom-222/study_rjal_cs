using System;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public class WarehouseForm : Form
    {
        private readonly WarehouseService _warehouseService;
        public Warehouse NewWarehouse { get; private set; }
        public Warehouse EditingWarehouse { get; private set; }
        private bool _isEditing = false;

        // 控件声明
        private Label label1;
        private Label label2;
        private TextBox txtWarehouseName;
        private TextBox txtAddress;
        private TextBox txtStatus;
        private DateTimePicker dtpCreatedDate;
        private DateTimePicker dtpLastUpdated;
        private Button btnSave;
        private Button btnCancel;

        public WarehouseForm()
        {
            _warehouseService = new WarehouseService();
            InitializeComponent();
            InitializeControls();
        }

        public WarehouseForm(Warehouse warehouse)
        {
            _warehouseService = new WarehouseService();
            EditingWarehouse = warehouse;
            _isEditing = true;
            InitializeComponent();
            InitializeControls();
            LoadWarehouseData();
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

        private void LoadWarehouseData()
        {
            if (EditingWarehouse != null)
            {
                this.Text = "编辑仓库";
                txtWarehouseName.Text = EditingWarehouse.WarehouseName;
                txtAddress.Text = EditingWarehouse.Address;
                txtStatus.Text = EditingWarehouse.Status;
                dtpCreatedDate.Value = EditingWarehouse.CreatedDate;
                dtpLastUpdated.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
            {
                MessageBox.Show("请输入仓库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWarehouseName.Focus();
                return;
            }

            try
            {
                if (_isEditing)
                {
                    // 更新现有仓库
                    EditingWarehouse.WarehouseName = txtWarehouseName.Text.Trim();
                    EditingWarehouse.Address = txtAddress.Text.Trim();
                    EditingWarehouse.Status = txtStatus.Text.Trim();
                    EditingWarehouse.LastUpdated = DateTime.Now;

                    _warehouseService.UpdateWarehouse(EditingWarehouse);
                    MessageBox.Show("仓库更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建新仓库
                    var warehouse = new Warehouse
                    {
                        WarehouseName = txtWarehouseName.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Status = txtStatus.Text.Trim(),
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now
                    };

                    int newWarehouseId = _warehouseService.CreateWarehouse(warehouse);
                    warehouse.WarehouseId = newWarehouseId;
                    NewWarehouse = warehouse;

                    MessageBox.Show("仓库添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = _isEditing ? "更新仓库失败: " : "添加仓库失败: ";
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
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
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
            this.label1.Text = "仓库名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "地址";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Location = new System.Drawing.Point(120, 12);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(200, 20);
            this.txtWarehouseName.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 38);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 60);
            this.txtAddress.TabIndex = 3;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 104);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 20);
            this.txtStatus.TabIndex = 4;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(120, 130);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreatedDate.TabIndex = 5;
            // 
            // dtpLastUpdated
            // 
            this.dtpLastUpdated.Location = new System.Drawing.Point(120, 156);
            this.dtpLastUpdated.Name = "dtpLastUpdated";
            this.dtpLastUpdated.Size = new System.Drawing.Size(200, 20);
            this.dtpLastUpdated.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 186);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(241, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WarehouseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 225);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpLastUpdated);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtWarehouseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WarehouseForm";
            this.Text = "添加仓库";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}