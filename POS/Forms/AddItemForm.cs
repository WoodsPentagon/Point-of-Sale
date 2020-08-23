using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using POS.Misc;

namespace POS.Forms
{
    public partial class AddItemForm : ItemFormBase
    {
        public AddItemForm()
        {
            InitializeComponent();
        }
        public AddItemForm(string initialBarcode)
        {
            InitializeComponent();
            barcode.Text = initialBarcode;
        }

        public override void Init()
        {
            base.Init();
            for (int i = 0; i < (int)ItemType.Count; i++)
            {
                itemType.Items.Add((ItemType)i).ToString();
            }
            itemType.SelectedIndex = 0;

            vSupplier.Items.Clear();
            using (var p = new POSEntities())
            {
                vSupplier.Items.AddRange(p.Suppliers.Select(x => x.Name).ToArray());
            }
        }

        private void barcode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(barcode.Text))
            {
                return;
            }
            using (var p = new POSEntities())
            {
                var i = p.Items.FirstOrDefault(x => x.Barcode == barcode.Text);
                if (i != null)
                {
                    this.ActiveControl = barcode;
                    barcode.SelectAll();
                    MessageBox.Show("Barcode already taken.");
                    return;
                }
            }
        }


        public override void save()
        {
            if (!canSave())
            {
                return;
            }
            using (var p = new POSEntities())
            {
                if (p.Items.FirstOrDefault(x => x.Barcode == barcode.Text) != null)
                {
                    MessageBox.Show("Barcode already taken.");
                    return;
                }
            }
            switch (MessageBox.Show(this, "Are you sure you want to create this item?", "Please double check.", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //Stay on this form
                case DialogResult.No:
                    return;

                default:
                    break;
            }

            var item = new Item();
            item.Barcode = barcode.Text;
            item.Name = name.Text;

            item.SellingPrice = sellingPrice.Value;
            item.DefaultCost = defaultCost.Value;

            item.Department = string.IsNullOrEmpty(itemDepartment.Text) ? null : itemDepartment.Text;
            item.Type = itemType.SelectedIndex;
            item.Details = string.IsNullOrEmpty(details.Text) ? null : details.Text;

            if (ImageBox.Image != null)
                item.SampleImage = ImageDatabaseConverter.imageToByteArray(ImageBox.Image);

            try
            {
                using (var p = new POSEntities())
                {
                    p.Items.Add(item);
                    ///not a hardware type
                    if (item.Type != 0)
                    {
                        var prod = new Product();
                        prod.Item = item;
                        prod.Quantity = 0;
                        prod.Supplier = null;
                        prod.Cost = null;
                        p.Products.Add(prod);
                    }                   

                    foreach (DataGridViewRow i in variationsTable.Rows)
                    {
                        var newVariation = new ItemVariation();

                        newVariation.Item = item;
                        newVariation.Supplier = p.Suppliers.FirstOrDefault(x => x.Name == (i.Cells[0].Value.ToString()));
                        newVariation.Cost = vCost.Value;
                        p.ItemVariations.Add(newVariation);
                    }

                    p.SaveChanges();
                    MessageBox.Show("Item created.");
                }
            }
            catch { }

            clearFields();
            this.Close();
            InvokeEvent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(vSupplier.Text))
            {
                return;
            }

            variationsTable.Rows.Add(vSupplier.Text, vCost.Value);
            vSupplier.Items.RemoveAt(vSupplier.SelectedIndex);
        }

        private void variationsTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var supplier = e.Row.Cells[0].Value.ToString();
            vSupplier.Items.Add(supplier);
        }
        void DisableVariations()
        {
            variationsTable.Rows.Clear();
            groupBox9.Enabled = false;
        }
        private void itemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(itemType.SelectedIndex != 0)
            {
                DisableVariations();
            }
            else
            {
                groupBox9.Enabled = true;

                vSupplier.Items.Clear();
                using (var p = new POSEntities())
                {
                    vSupplier.Items.AddRange(p.Suppliers.Select(x => x.Name).ToArray());
                }
            }
        }
    }
}
