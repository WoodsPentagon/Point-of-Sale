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
    public partial class EditItemForm : ItemFormBase
    {
        Item target;
        public EditItemForm()
        {
            InitializeComponent();
        }
        public override bool canSave()
        {
            return base.canSave();
        }
        public override void Init()
        {
            base.Init();

            itemType.Enabled = false;
          
            using (var p = new POSEntities())
            {               
                ImageBox.Image = POS.Misc.ImageDatabaseConverter.byteArrayToImage(p.Items.FirstOrDefault(x=>x.Barcode == target.Barcode).SampleImage);
            }

            name.Text = target.Name;
            sellingPrice.Value = target.SellingPrice;
            itemDepartment.Text = target.Department;
            details.Text = target.Details;
        }
        public void GetBarcode(string item)
        {
            using (var p = new POSEntities())
            {
                target = p.Items.FirstOrDefault(x => x.Barcode == item);
            }           
        }
        public override void save()
        {
            try
            {
                using (var p = new POS.POSEntities())
                {
                    var item = p.Items.FirstOrDefault(x => x.Barcode == barcode.Text);
                    item.Name = name.Text;
                    item.DefaultCost = defaultCost.Value;
                    item.SellingPrice = sellingPrice.Value;
                    item.Department = itemDepartment.Text;
                    item.Details = details.Text;

                    if (ImageBox.Image != null)
                    {
                        item.SampleImage = Misc.ImageDatabaseConverter.imageToByteArray(ImageBox.Image);
                    }

                    p.SaveChanges();
                    InvokeEvent();
                    MessageBox.Show("Successfully saved.");
                    this.Close();
                }
            }
            catch
            {

            }
        }
    }
}
