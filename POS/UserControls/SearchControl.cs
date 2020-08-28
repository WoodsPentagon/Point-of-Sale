using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Misc;

namespace POS.UserControls
{
    public partial class SearchControl : UserControl
    {
        public bool SearchDone = false;
        /// <summary>
        /// triggers upon hitting enter or pressing search Button;
        /// </summary>
        public event EventHandler<SearchEventArgs> OnSearch;
        /// <summary>
        /// triggers when search bar text become empty and a search has been done
        /// </summary>
        public event EventHandler OnTextEmpty;
        public string SearchText
        {
            get
            {
                return searchText.Text;
            }
        }
        public SearchControl()
        {
            InitializeComponent();
        }
        public void SetAutoComplete(params string[] values)
        {
            searchText.AutoCompleteCustomSource.Clear();
            searchText.AutoCompleteCustomSource.AddRange(values);
            //foreach(var i in searchText.AutoCompleteCustomSource)
            //{
            //    Console.WriteLine(i.ToString());
            //}
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (searchText.Text == string.Empty)
                return;

           // SearchDone = true;
            OnSearch?.Invoke(this, new SearchEventArgs(this));

        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            if (searchText.TextLength == 0 && SearchDone)
                OnTextEmpty?.Invoke(this, null);
        }

        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                searchBtn.PerformClick();
        }
    }
}
