using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class InventoryItemView : Form
    {
        public InventoryItemView()
        {
            InitializeComponent();
        }
        public void SetItemId(string barcode)
        {
            using (var p = new POSEntities())
            {
                var item = p.Items.FirstOrDefault(x => x.Barcode == barcode);
                if (item == null)
                {
                    MessageBox.Show("Not found.");
                    return;
                }
                barcodeField.Text = item.Barcode;
                itemName.Text = item.Name;
                sellingPrice.Text = string.Format("₱ {0:n}", item.SellingPrice);

                var invItem = p.Products.Where(x => x.Item.Barcode == item.Barcode);

                var sum = invItem.Sum(x => x.Quantity);
                quantity.Text = sum == 0 ? "Infinite" : sum.ToString();
                int counter = 0;
                foreach (var i in invItem)
                {
                    counter++;

                    Console.WriteLine(i.SupplierId);

                    invTable.Rows.Add(counter, i.SerialNumber, i.Quantity == 0?"Infinite":i.Quantity.ToString(), (i.Supplier?.Name) ?? "NOT SPECIFIED");
                }
            }
        }

        Product target;
        private void invTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (!UserManager.instance.currentLogin.CanEditProduct ?? false)
            //{
            //    e.Cancel = true;
            //    return;
            //}
            if (e.ColumnIndex != 1)
            {
                return;
            }
            var dgt = sender as DataGridView;
            var serial = dgt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            var quantity = Convert.ToInt32(dgt.Rows[e.RowIndex].Cells[2].Value.ToString());
            if (string.IsNullOrEmpty(serial) && quantity > 1)
            {
                MessageBox.Show("Thes serial cannot be edited");
                e.Cancel = true;
                return;
            }

            using (var p = new POSEntities())
            {
                target = p.Products.FirstOrDefault(x => x.SerialNumber == serial);

            }
        }

        private void invTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgt = sender as DataGridView;
            if (dgt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() == target.SerialNumber)
            {
                return;
            }
            using (var p = new POSEntities())
            {
                if (MessageBox.Show("Are you sure you want to save new Serial?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    dgt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = target.SerialNumber;
                }
                else
                {
                    var t = p.Products.FirstOrDefault(x => x.SerialNumber == target.SerialNumber);
                    t.SerialNumber = dgt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                    p.SaveChanges();
                    //OnSave?.Invoke(this, null);
                    MessageBox.Show("Serial successfully updated");
                }
            }

        }
    }
}
