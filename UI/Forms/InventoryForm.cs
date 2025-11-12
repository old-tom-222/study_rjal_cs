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
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductSku", HeaderText = "商品编码" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "商品名称" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseId", HeaderText = "仓库ID" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WarehouseName", HeaderText = "仓库名称" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "库存数量" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SafeStock", HeaderText = "安全库存" });
            gridInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LastUpdated", HeaderText = "最后更新" });
        }

        private void LoadAllInventory()
        {
            var list = _service.QueryInventory(null, null);
            gridInventory.DataSource = list;
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
            var list = _service.QueryTransactions(productId, warehouseId, from, to);
            gridTxn.AutoGenerateColumns = true;
            gridTxn.DataSource = list;
        }

        private void BtnLoadWarningsClick(object sender, EventArgs e)
        {
            var list = _service.GetLowStockWarnings();
            gridWarnings.AutoGenerateColumns = true;
            gridWarnings.DataSource = list;
        }

        private int? ParseNullableInt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            if (int.TryParse(text, out var val)) return val;
            return null;
        }
    }
}