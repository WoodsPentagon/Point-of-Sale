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

        public override bool canSave()
        {
            if (string.IsNullOrEmpty(barcode.Text) || string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("Barcode and Item name can never be empty!");
                return false;
            }

            return true;
        }
        public override void save()
        {
            if (!canSave())
            {
                return;
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
            item.Details = string.IsNullOrEmpty(details.Text)?null:details.Text;
            if (ImageBox.Image != null)
            {
                item.SampleImage = ImageDatabaseConverter.imageToByteArray(ImageBox.Image);
            }

            try
            {
                using (var p = new POSEntities())
                {
                    p.Items.Add(item);
                    p.SaveChanges();
                    MessageBox.Show("Item created.");
                }
            }
            catch
            {

            }
        }
    }
}
