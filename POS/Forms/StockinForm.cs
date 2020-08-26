using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Misc;

namespace POS.Forms
{
    public partial class StockinForm : Form
    {
        List<Product> productsToImport = new List<Product>();

        public event EventHandler OnSave;

        public StockinForm()
        {
            InitializeComponent();
        }

        void SetTable()
        {
            itemsTable.Rows.Clear();
            using (var p = new POSEntities())
            {               
                foreach (var i in p.Items.Where(x => x.Type == (int)ItemType.Hardware))
                {
                    itemsTable.Rows.Add(i.Barcode, i.Name, i.DefaultCost);
                }
            }           
        }
        private void StockinForm_Load(object sender, EventArgs e)
        {
            SetTable();           
        }

        private void itemsTable_SelectionChanged(object sender, EventArgs e)
        {
            if (itemsTable.DataGridViewCurrentRowIndex() == -1)
                return;
            using (var p = new POSEntities())
            {

                ///// get the current row
                var currentRow = itemsTable.Rows[itemsTable.DataGridViewCurrentRowIndex()];
                //// get the barcode
                var barc = currentRow.Cells[0].Value.ToString();

                var item = p.Items.FirstOrDefault(x => x.Barcode == barc);

                barcode.Text = item.Barcode;
                itemName.Text = item.Name;
                cost.Value = item.DefaultCost;

                supplier.Items.Clear();
                supplier.IntegralHeight = false;


                var s = p.ItemVariations.Where(x => x.Item.Barcode == item.Barcode).Select(x => x.Supplier.Name).ToArray();
                supplier.Items.AddRange(s);
                supplier.AutoCompleteCustomSource.AddRange(s);

            }
            serialNumber.Text = string.Empty;
            this.ActiveControl = serialNumber;
        }

        bool alreadyInTable(string id, string supplier, out int index)
        {
            string b, s;
            for (int i = 0; i < toStockTable.RowCount; i++)
            {

                b = toStockTable.Rows[i].Cells[0].Value.ToString();
                s = toStockTable.Rows[i].Cells[6].Value.ToString();
                if (b == id && s == supplier && string.IsNullOrEmpty(toStockTable.Rows[i].Cells[1].Value?.ToString()))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        Item getItemById(string id)
        {
            using (var p = new POSEntities())
            {
                return p.Items.FirstOrDefault(x => x.Barcode == id);
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            this.ActiveControl = search;
            addItem();
        }

        void addItem()
        {

            ///check if the item to be added has serial
            if (string.IsNullOrEmpty(supplier.Text))
            {
                ActiveControl = supplier;
                MessageBox.Show("Supplier is required.");
                return;
            }
            if (!string.IsNullOrEmpty(serialNumber.Text))
            {
                if (serialAlreadyTaken())
                {
                    return;
                }
            }
            else
            {
                ///if not then check if the item to be added is already in the table, if yes just edit quantitty
                int index;
                if (alreadyInTable(barcode.Text, supplier.Text, out index))
                {
                    var currentQuant = Convert.ToInt32(toStockTable.Rows[index].Cells[3].Value);
                    toStockTable.Rows[index].Cells[3].Value = currentQuant + (int)quantity.Value;
                    toStockTable.Rows[index].Cells[5].Value = (currentQuant + quantity.Value) * cost.Value;
                    return;
                }
            }
            ///else, add the item
            Decimal totalCost = quantity.Value * cost.Value;
            toStockTable.Rows.Add(barcode.Text, string.IsNullOrEmpty(serialNumber.Text) ? null : serialNumber.Text, itemName.Text, (int)quantity.Value, cost.Value, totalCost.ToString(), supplier.Text);

            serialNumber.Text = string.Empty;
        }

        private void stockinBtn_Click(object sender, EventArgs e)
        {
            using (var p = new POSEntities())
            {

                for (int i = 0; i < toStockTable.RowCount; i++)
                {
                    var barcode = toStockTable.Rows[i].Cells[0].Value.ToString();
                    var serial = toStockTable.Rows[i].Cells[1].Value?.ToString();
                    var cost = toStockTable.Rows[i].Cells[4].Value;
                    var supp = toStockTable.Rows[i].Cells[6].Value.ToString();

                    ///if serial is present, then push with new entity
                    Product newProduct;
                    if (!string.IsNullOrEmpty(serial))
                    {
                        if (p.Products.FirstOrDefault(x => x.SerialNumber == serial) != null)
                        {
                            MessageBox.Show("Item with serial " + serial + " already in Inventory");
                        }
                        else
                        {
                            newProduct = new Product();
                            newProduct.SerialNumber = serial;
                            newProduct.Item = p.Items.FirstOrDefault(x => x.Barcode == barcode);
                            newProduct.Quantity = 1;
                            newProduct.Supplier = p.Suppliers.FirstOrDefault(x => x.Name == supp);
                            p.Products.Add(newProduct);
                        }
                    }
                    else
                    {
                        newProduct = p.Products.FirstOrDefault(x => x.Item.Barcode == barcode && x.SerialNumber == null);
                        var quantity = toStockTable.Rows[i].Cells[3].Value;
                        if (newProduct != null)
                        {
                            //newProduct.Quantity = newProduct.Quantity + (int)quantity;
                            newProduct.Quantity += (int)quantity;

                        }
                        else
                        {
                            newProduct = new Product();
                            newProduct.Item = p.Items.FirstOrDefault(x => x.Barcode == barcode);
                            newProduct.Quantity = (int)quantity;
                            p.Products.Add(newProduct);
                        }
                    }
                    ////add stockin history here

                    //var stockinHist = new StockinHistory();
                    //stockinHist.ItemName = it.Product.Item.Name;
                    //stockinHist.Cost = it.Product.Cost;
                    //stockinHist.Supplier = it.Product.Supplier.Name;
                    //stockinHist.Date = DateTime.Now;
                    //stockinHist.Quantity = q;
                    //stockinHist.SerialNumber = it.SerialNumber;
                    //stockinHist.LoginUsername = p.Logins.FirstOrDefault(x => x.Username == UserManager.instance.currentLogin.Username).Username;

                    //p.StockinHistories.Add(stockinHist);
                }
                p.SaveChanges();
                OnSave?.Invoke(this, null);
                MessageBox.Show("Saved.");
                this.Close();
            }
        }

        bool serialAlreadyTaken()
        {
            using (var p = new POSEntities())
            {
                if (p.Products.FirstOrDefault(x => x.SerialNumber == serialNumber.Text) != null)
                {
                    MessageBox.Show("Serial number already in inventory.");
                    return true;
                }
            }
            for (int i = 0; i < toStockTable.RowCount; i++)
            {
                if (serialNumber.Text == toStockTable.Rows[i].Cells[1].Value.ToString())
                {
                    MessageBox.Show("Serial number already on the list.");
                    return true;
                }
            }
            return false;
        }

        private void itemsTable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            addItem();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (toStockTable.RowCount == 0)
                return;
            int index = toStockTable.CurrentCell.RowIndex;
            toStockTable.Rows.RemoveAt(index);
        }

        private void serialNumber_TextChanged(object sender, EventArgs e)
        {
            if (serialNumber.Text.Count() != 0)
            {
                quantity.Value = 1;
                quantity.Enabled = false;
                return;
            }
            quantity.Enabled = true;
        }

        private void barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (search.Text == string.Empty)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                searchBtn.PerformClick();
            }
        }

        private void barcode_TextChanged(object sender, EventArgs e)
        {
            if (search.TextLength <= 0)
                SetTable();
        }

        private void StockinForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
                this.ActiveControl = search;
            if (e.Shift && e.KeyCode == Keys.Enter)
                addBtn.PerformClick();

            if (e.KeyCode == Keys.F2)
                this.ActiveControl = serialNumber;

            if (e.KeyCode == Keys.F3)
                this.ActiveControl = quantity;
        }

        private void createItemBtn_Click(object sender, EventArgs e)
        {
            using (var additem = new AddItemForm())
            {
                additem.OnSave += Additem_OnSave;
                additem.ShowDialog();
            }
        }

        private void Additem_OnSave(object sender, EventArgs e)
        {
            SetTable();
        }

        private void supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var p = new POSEntities())
            {
                var v = p.ItemVariations.FirstOrDefault(x => x.Item.Barcode == barcode.Text && x.Supplier.Name == supplier.Text);
                cost.Value = v.Cost;
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (string.Equals(search.Text, string.Empty))
            {
                return;
            }
            itemsTable.Rows.Clear();
            using (var p = new POSEntities())
            {
                var items = p.Items.Where(x => x.Barcode == search.Text && x.Type == 0);
                if (items.Count() == 0)
                {
                    items = p.Items.Where(x => x.Name.Contains(search.Text) && x.Type == 0);
                }
                if(items.Count()==0)
                {
                    MessageBox.Show("Entry not found.");
                    return;
                }
                foreach (var i in items)
                {
                    itemsTable.Rows.Add(i.Barcode, i.Name, i.DefaultCost);
                }
            }
        }

        private void supplier_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(supplier.Text))
                return;
            using (var p = new POSEntities())
            {
                var s = p.ItemVariations.FirstOrDefault(x => x.Item.Barcode == barcode.Text && x.Supplier.Name == supplier.Text);
                if (s == null)
                {
                    if (MessageBox.Show("Would you like to add an item variation?", "Variation of this item with this supplier is not found.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.ActiveControl = supplier;
                        using (var i = new AddProductForm(barcode.Text))
                        {
                            i.Onsave += I_Onsave;
                            i.ShowDialog();
                        }
                    }
                    else
                    {
                        supplier.ResetText();
                    }
                }
            }
        }

        private void I_Onsave(object sender, EventArgs e)
        {
            supplier.Items.Clear();
            supplier.AutoCompleteCustomSource.Clear();
            using (var p = new POSEntities())
            {
                var s = p.ItemVariations.Where(x => x.Item.Barcode == barcode.Text).Select(x => x.Supplier.Name).ToArray();
                supplier.Items.AddRange(s);
                supplier.AutoCompleteCustomSource.AddRange(s);
            }
        }
    }
}
