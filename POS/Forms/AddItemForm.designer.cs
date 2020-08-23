namespace POS.Forms
{
    partial class AddItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.add = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.vCost = new System.Windows.Forms.NumericUpDown();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.vSupplier = new System.Windows.Forms.ComboBox();
            this.variationsTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultCost)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vCost)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.variationsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // barcode
            // 
            this.barcode.Leave += new System.EventHandler(this.barcode_Leave);
            // 
            // details
            // 
            this.details.Size = new System.Drawing.Size(301, 221);
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(480, 196);
            this.groupBox6.Size = new System.Drawing.Size(314, 247);
            // 
            // itemType
            // 
            this.itemType.SelectedIndexChanged += new System.EventHandler(this.itemType_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.add);
            this.groupBox9.Controls.Add(this.groupBox11);
            this.groupBox9.Controls.Add(this.groupBox10);
            this.groupBox9.Controls.Add(this.variationsTable);
            this.groupBox9.Location = new System.Drawing.Point(12, 196);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(462, 247);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Item Variations ";
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add.Image = ((System.Drawing.Image)(resources.GetObject("add.Image")));
            this.add.Location = new System.Drawing.Point(419, 30);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(37, 31);
            this.add.TabIndex = 3;
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.vCost);
            this.groupBox11.Location = new System.Drawing.Point(302, 20);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(111, 48);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Cost";
            // 
            // vCost
            // 
            this.vCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vCost.DecimalPlaces = 2;
            this.vCost.Location = new System.Drawing.Point(6, 21);
            this.vCost.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.vCost.Name = "vCost";
            this.vCost.Size = new System.Drawing.Size(99, 20);
            this.vCost.TabIndex = 0;
            this.vCost.ThousandsSeparator = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.vSupplier);
            this.groupBox10.Location = new System.Drawing.Point(7, 19);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(289, 48);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Suppiler";
            // 
            // vSupplier
            // 
            this.vSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vSupplier.FormattingEnabled = true;
            this.vSupplier.Location = new System.Drawing.Point(6, 21);
            this.vSupplier.Name = "vSupplier";
            this.vSupplier.Size = new System.Drawing.Size(277, 21);
            this.vSupplier.TabIndex = 0;
            // 
            // variationsTable
            // 
            this.variationsTable.AllowUserToAddRows = false;
            this.variationsTable.AllowUserToResizeColumns = false;
            this.variationsTable.AllowUserToResizeRows = false;
            this.variationsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.variationsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.variationsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.variationsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.variationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.variationsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.variationsTable.DefaultCellStyle = dataGridViewCellStyle1;
            this.variationsTable.Location = new System.Drawing.Point(13, 73);
            this.variationsTable.MultiSelect = false;
            this.variationsTable.Name = "variationsTable";
            this.variationsTable.ReadOnly = true;
            this.variationsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.variationsTable.RowHeadersVisible = false;
            this.variationsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.variationsTable.Size = new System.Drawing.Size(443, 168);
            this.variationsTable.TabIndex = 0;
            this.variationsTable.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.variationsTable_UserDeletingRow);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Supplier";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Cost";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 53;
            // 
            // AddItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(803, 484);
            this.Controls.Add(this.groupBox9);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddItemForm";
            this.Controls.SetChildIndex(this.takePhotoBtn, 0);
            this.Controls.SetChildIndex(this.groupBox6, 0);
            this.Controls.SetChildIndex(this.groupBox9, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultCost)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vCost)).EndInit();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.variationsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.NumericUpDown vCost;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox vSupplier;
        protected System.Windows.Forms.DataGridView variationsTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}