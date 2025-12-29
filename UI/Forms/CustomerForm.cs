using System;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        public Customer NewCustomer { get; private set; }
        public Customer EditingCustomer { get; private set; }
        private bool _isEditing = false;

        public CustomerForm()
    {
        // 调用InitializeComponent()创建控件，然后调用InitializeControls设置默认值
        _customerService = new CustomerService();
        InitializeComponent();
        InitializeControls();
    }
    
    public CustomerForm(Customer customer)
    {
        _customerService = new CustomerService();
        EditingCustomer = customer;
        _isEditing = true;
        InitializeComponent();
        InitializeControls();
        LoadCustomerData();
    }

        private void InitializeControls()
        {
            // 设置默认值
            txtStatus.Text = "ACTIVE";
            dtpCreatedDate.Value = DateTime.Now;
            dtpLastUpdated.Value = DateTime.Now;
            dtpCreatedDate.Enabled = false;
            dtpLastUpdated.Enabled = false;
        }
        
        private void LoadCustomerData()
        {
            if (EditingCustomer != null)
            {
                this.Text = "编辑客户";
                txtCustomerCode.Text = EditingCustomer.CustomerCode;
                txtCustomerName.Text = EditingCustomer.CustomerName;
                txtContactPerson.Text = EditingCustomer.ContactPerson;
                txtContactPhone.Text = EditingCustomer.ContactPhone;
                txtEmail.Text = EditingCustomer.Email;
                txtAddress.Text = EditingCustomer.Address;
                txtCity.Text = EditingCustomer.City;
                txtProvince.Text = EditingCustomer.Province;
                txtPostalCode.Text = EditingCustomer.PostalCode;
                txtCustomerType.Text = EditingCustomer.CustomerType;
                txtStatus.Text = EditingCustomer.Status;
                txtNotes.Text = EditingCustomer.Notes;
                dtpCreatedDate.Value = EditingCustomer.CreatedDate;
                dtpLastUpdated.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 验证必填字段
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("请输入客户名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomerName.Focus();
                return;
            }

            try
            {
                if (_isEditing)
                {
                    // 更新现有客户
                    EditingCustomer.CustomerCode = txtCustomerCode.Text.Trim();
                    EditingCustomer.CustomerName = txtCustomerName.Text.Trim();
                    EditingCustomer.ContactPerson = txtContactPerson.Text.Trim();
                    EditingCustomer.ContactPhone = txtContactPhone.Text.Trim();
                    EditingCustomer.Email = txtEmail.Text.Trim();
                    EditingCustomer.Address = txtAddress.Text.Trim();
                    EditingCustomer.City = txtCity.Text.Trim();
                    EditingCustomer.Province = txtProvince.Text.Trim();
                    EditingCustomer.PostalCode = txtPostalCode.Text.Trim();
                    EditingCustomer.CustomerType = txtCustomerType.Text.Trim();
                    EditingCustomer.Status = txtStatus.Text.Trim();
                    EditingCustomer.LastUpdated = DateTime.Now;
                    EditingCustomer.Notes = txtNotes.Text.Trim();

                    _customerService.UpdateCustomer(EditingCustomer);
                    MessageBox.Show("客户更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建新客户
                    var customer = new Customer
                    {
                        CustomerCode = txtCustomerCode.Text.Trim(),
                        CustomerName = txtCustomerName.Text.Trim(),
                        ContactPerson = txtContactPerson.Text.Trim(),
                        ContactPhone = txtContactPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        City = txtCity.Text.Trim(),
                        Province = txtProvince.Text.Trim(),
                        PostalCode = txtPostalCode.Text.Trim(),
                        CustomerType = txtCustomerType.Text.Trim(),
                        Status = txtStatus.Text.Trim(),
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now,
                        Notes = txtNotes.Text.Trim()
                    };

                    // 保存客户
                    int newCustomerId = _customerService.CreateCustomer(customer);
                    customer.CustomerId = newCustomerId;
                    NewCustomer = customer;

                    MessageBox.Show("客户添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = _isEditing ? "更新客户失败: " : "添加客户失败: ";
                MessageBox.Show(errorMessage + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 以下是Form Designer生成的代码，实际使用时会被自动生成
        // 这里仅作为参考
        private System.ComponentModel.IContainer components = null;
        private TextBox txtCustomerCode;
        private TextBox txtCustomerName;
        private TextBox txtContactPerson;
        private TextBox txtContactPhone;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private TextBox txtCity;
        private TextBox txtProvince;
        private TextBox txtPostalCode;
        private TextBox txtCustomerType;
        private TextBox txtStatus;
        private DateTimePicker dtpCreatedDate;
        private DateTimePicker dtpLastUpdated;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtProvince = new System.Windows.Forms.TextBox();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.txtCustomerType = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.dtpLastUpdated = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(120, 12);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerCode.TabIndex = 0;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(120, 38);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerName.TabIndex = 1;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(120, 64);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(200, 20);
            this.txtContactPerson.TabIndex = 2;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(120, 90);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(200, 20);
            this.txtContactPhone.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 116);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 142);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 20);
            this.txtAddress.TabIndex = 5;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(120, 168);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(200, 20);
            this.txtCity.TabIndex = 6;
            // 
            // txtProvince
            // 
            this.txtProvince.Location = new System.Drawing.Point(120, 194);
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.Size = new System.Drawing.Size(200, 20);
            this.txtProvince.TabIndex = 7;
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Location = new System.Drawing.Point(120, 220);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(200, 20);
            this.txtPostalCode.TabIndex = 8;
            // 
            // txtCustomerType
            // 
            this.txtCustomerType.Location = new System.Drawing.Point(120, 246);
            this.txtCustomerType.Name = "txtCustomerType";
            this.txtCustomerType.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerType.TabIndex = 9;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 272);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(200, 20);
            this.txtStatus.TabIndex = 10;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(120, 298);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(200, 20);
            this.dtpCreatedDate.TabIndex = 11;
            // 
            // dtpLastUpdated
            // 
            this.dtpLastUpdated.Location = new System.Drawing.Point(120, 324);
            this.dtpLastUpdated.Name = "dtpLastUpdated";
            this.dtpLastUpdated.Size = new System.Drawing.Size(200, 20);
            this.dtpLastUpdated.TabIndex = 12;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(120, 350);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(200, 60);
            this.txtNotes.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 426);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(241, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "客户代码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "客户名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "联系人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "联系电话";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "邮箱";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "地址";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "城市";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "省份";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "邮编";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "客户类型";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 275);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "状态";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 301);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "创建日期";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 327);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "更新日期";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 353);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "备注";
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 461);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.dtpLastUpdated);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtCustomerType);
            this.Controls.Add(this.txtPostalCode);
            this.Controls.Add(this.txtProvince);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtCustomerCode);
            this.Name = "CustomerForm";
            this.Text = "添加客户";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}