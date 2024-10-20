namespace POS_System
{
    partial class CashierForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSale = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelItems = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxSelectedItems = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelCart = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTotalAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCustomerPayment = new System.Windows.Forms.TextBox();
            this.Change = new System.Windows.Forms.Label();
            this.textBoxChange = new System.Windows.Forms.TextBox();
            this.buttonCheckout = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonPrintRecipt = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxSale.SuspendLayout();
            this.groupBoxSelectedItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(860, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cashier Dashboard";
            // 
            // groupBoxSale
            // 
            this.groupBoxSale.Controls.Add(this.flowLayoutPanelItems);
            this.groupBoxSale.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSale.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBoxSale.Location = new System.Drawing.Point(33, 79);
            this.groupBoxSale.Name = "groupBoxSale";
            this.groupBoxSale.Size = new System.Drawing.Size(2081, 589);
            this.groupBoxSale.TabIndex = 1;
            this.groupBoxSale.TabStop = false;
            this.groupBoxSale.Text = "Sale";
            // 
            // flowLayoutPanelItems
            // 
            this.flowLayoutPanelItems.AutoScroll = true;
            this.flowLayoutPanelItems.BackColor = System.Drawing.Color.DarkGray;
            this.flowLayoutPanelItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelItems.Location = new System.Drawing.Point(3, 30);
            this.flowLayoutPanelItems.Name = "flowLayoutPanelItems";
            this.flowLayoutPanelItems.Size = new System.Drawing.Size(2075, 556);
            this.flowLayoutPanelItems.TabIndex = 0;
            // 
            // groupBoxSelectedItems
            // 
            this.groupBoxSelectedItems.Controls.Add(this.flowLayoutPanelCart);
            this.groupBoxSelectedItems.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSelectedItems.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBoxSelectedItems.Location = new System.Drawing.Point(36, 685);
            this.groupBoxSelectedItems.Name = "groupBoxSelectedItems";
            this.groupBoxSelectedItems.Size = new System.Drawing.Size(2075, 294);
            this.groupBoxSelectedItems.TabIndex = 2;
            this.groupBoxSelectedItems.TabStop = false;
            this.groupBoxSelectedItems.Text = "Selected Items";
            // 
            // flowLayoutPanelCart
            // 
            this.flowLayoutPanelCart.AutoScroll = true;
            this.flowLayoutPanelCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCart.Location = new System.Drawing.Point(3, 29);
            this.flowLayoutPanelCart.Name = "flowLayoutPanelCart";
            this.flowLayoutPanelCart.Size = new System.Drawing.Size(2069, 262);
            this.flowLayoutPanelCart.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label2.Location = new System.Drawing.Point(33, 1007);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Amount";
            // 
            // textBoxTotalAmount
            // 
            this.textBoxTotalAmount.Location = new System.Drawing.Point(268, 1007);
            this.textBoxTotalAmount.Name = "textBoxTotalAmount";
            this.textBoxTotalAmount.ReadOnly = true;
            this.textBoxTotalAmount.Size = new System.Drawing.Size(333, 26);
            this.textBoxTotalAmount.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label3.Location = new System.Drawing.Point(33, 1055);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer Payment";
            // 
            // textBoxCustomerPayment
            // 
            this.textBoxCustomerPayment.Location = new System.Drawing.Point(268, 1055);
            this.textBoxCustomerPayment.Name = "textBoxCustomerPayment";
            this.textBoxCustomerPayment.Size = new System.Drawing.Size(333, 26);
            this.textBoxCustomerPayment.TabIndex = 6;
            this.textBoxCustomerPayment.TextChanged += new System.EventHandler(this.textBoxCustomerPayment_TextChanged);
            // 
            // Change
            // 
            this.Change.AutoSize = true;
            this.Change.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Change.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.Change.Location = new System.Drawing.Point(34, 1101);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(88, 27);
            this.Change.TabIndex = 7;
            this.Change.Text = "Change";
            // 
            // textBoxChange
            // 
            this.textBoxChange.Location = new System.Drawing.Point(268, 1101);
            this.textBoxChange.Name = "textBoxChange";
            this.textBoxChange.ReadOnly = true;
            this.textBoxChange.Size = new System.Drawing.Size(333, 26);
            this.textBoxChange.TabIndex = 8;
            // 
            // buttonCheckout
            // 
            this.buttonCheckout.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckout.ForeColor = System.Drawing.Color.Green;
            this.buttonCheckout.Location = new System.Drawing.Point(475, 1134);
            this.buttonCheckout.Name = "buttonCheckout";
            this.buttonCheckout.Size = new System.Drawing.Size(126, 40);
            this.buttonCheckout.TabIndex = 9;
            this.buttonCheckout.Text = "Checkout";
            this.buttonCheckout.UseVisualStyleBackColor = true;
            this.buttonCheckout.Click += new System.EventHandler(this.buttonCheckout_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.Location = new System.Drawing.Point(268, 1134);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(109, 40);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear Sale";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(1885, 1113);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(110, 53);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Logout";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonPrintRecipt
            // 
            this.buttonPrintRecipt.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrintRecipt.Location = new System.Drawing.Point(633, 1134);
            this.buttonPrintRecipt.Name = "buttonPrintRecipt";
            this.buttonPrintRecipt.Size = new System.Drawing.Size(145, 40);
            this.buttonPrintRecipt.TabIndex = 12;
            this.buttonPrintRecipt.Text = "Print Recipt";
            this.buttonPrintRecipt.UseVisualStyleBackColor = true;
            this.buttonPrintRecipt.Click += new System.EventHandler(this.buttonPrintRecipt_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(2016, 1113);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(92, 54);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // CashierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(2151, 1195);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonPrintRecipt);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCheckout);
            this.Controls.Add(this.textBoxChange);
            this.Controls.Add(this.Change);
            this.Controls.Add(this.textBoxCustomerPayment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTotalAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxSelectedItems);
            this.Controls.Add(this.groupBoxSale);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Name = "CashierForm";
            this.Text = "CashierForm";
            this.groupBoxSale.ResumeLayout(false);
            this.groupBoxSelectedItems.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSale;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelItems;
        private System.Windows.Forms.GroupBox groupBoxSelectedItems;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTotalAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCustomerPayment;
        private System.Windows.Forms.Label Change;
        private System.Windows.Forms.TextBox textBoxChange;
        private System.Windows.Forms.Button buttonCheckout;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonPrintRecipt;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button buttonClose;
    }
}