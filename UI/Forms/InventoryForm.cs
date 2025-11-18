using System;
using System.Windows.Forms;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryService _service = new InventoryService();

        public InventoryForm()
        {
            InitializeComponent();
            this.Load += InventoryForm_Load;
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            SetupInventoryGrid();
            LoadAllInventory();
        }

        private void SetupInventoryGrid()
        {
            gridInventory.AutoGenerateColumns = false;
            gridInventory.Columns.Clear();
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "商品ID" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductSku", HeaderText = "商品SKU" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "商品名称" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseId", HeaderText = "仓库ID" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseName", HeaderText = "仓库名称" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "数量" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SafeStock", HeaderText = "安全库存" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LastUpdated", HeaderText = "最后更新" });
        }

        private void LoadAllInventory()
        {
            var list = _service.QueryInventory(null, null);
            gridInventory.DataSource = list;
        }

        private void SetupTransactionGrid()
        {
            gridTxn.AutoGenerateColumns = false;
            gridTxn.Columns.Clear();
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "流水ID" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "商品ID" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "商品名称" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductSku", HeaderText = "SKU" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseId", HeaderText = "仓库ID" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseName", HeaderText = "仓库名称" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ChangeQty", HeaderText = "变动数量" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "变动类型" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedAt", HeaderText = "变动日期" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reference", HeaderText = "参考ID" });
            gridTxn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Remark", HeaderText = "备注" });
        }

        private void SetupWarningGrid()
        {
            gridWarnings.AutoGenerateColumns = false;
            gridWarnings.Columns.Clear();
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "商品ID" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "商品名称" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductSku", HeaderText = "SKU" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseId", HeaderText = "仓库ID" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseName", HeaderText = "仓库名称" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "当前数量" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SafeStock", HeaderText = "安全库存" });
            gridWarnings.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LastUpdated", HeaderText = "最后更新" });
        }

        private void BtnQueryClick(object sender, EventArgs e)
        {
            int? productId = ParseNullableInt(txtProductId.Text);
            int? warehouseId = ParseNullableInt(txtWarehouseId.Text);
            SetupInventoryGrid();
            if (productId == null && warehouseId == null)
            {
                LoadAllInventory();
                return;
            }
            var list = _service.QueryInventory(productId, warehouseId);
            gridInventory.DataSource = list;
        }

        private void BtnTxnQueryClick(object sender, EventArgs e)
        {
            int? productId = ParseNullableInt(txtTxnProductId.Text);
            int? warehouseId = ParseNullableInt(txtTxnWarehouseId.Text);
            DateTime? from = chkUseTimeRange.Checked ? (DateTime?)dtFrom.Value.Date : null;
            DateTime? to = chkUseTimeRange.Checked ? (DateTime?)dtTo.Value.Date.AddDays(1).AddSeconds(-1) : null;
            SetupTransactionGrid();
            var list = _service.QueryTransactions(productId, warehouseId, from, to);
            gridTxn.DataSource = list;
        }

        private void BtnLoadWarningsClick(object sender, EventArgs e)
        {
            SetupWarningGrid();
            var list = _service.GetLowStockWarnings();
            gridWarnings.DataSource = list;
        }

        private int? ParseNullableInt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            int val;
            if (int.TryParse(text, out val)) return val;
            return null;
        }
    }
}
