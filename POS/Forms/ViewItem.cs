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
    public partial class ViewItem : Form
    {
        Item item;

        public void SetItemId(string Id)
        {
            using (var p = new POSEntities())
            {
                item = p.Items.FirstOrDefault(x => x.Barcode == Id);
                barcode.Text = item.Barcode;
                itemName.Text = item.Name;
                sellingPrice.Text = string.Format("₱ {0:n}", item.SellingPrice);
                defaultCost.Text = string.Format("₱ {0:n}", item.DefaultCost);
                itemType.Text = ((ItemType)item.Type).ToString();
                department.Text = item.Department;
                details.Text = item.Details??"NONE";

                ImageBox.Image = Misc.ImageDatabaseConverter.byteArrayToImage(item.SampleImage);

                foreach(var i in p.ItemVariations.Where(x=>x.ItemBarcode == item.Barcode))
                {
                    varTable.Rows.Add(i.Supplier.Name,string.Format("₱ {0:n}", i.Cost));
                }
            }
        }

        public ViewItem()
        {
            InitializeComponent();
        }
    }
}

