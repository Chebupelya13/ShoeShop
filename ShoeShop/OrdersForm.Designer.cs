namespace ShoeShop
{
    partial class OrdersForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.flpOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            
            // pnlTop
            this.pnlTop.Controls.Add(this.btnAddOrder);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(784, 50);
            this.pnlTop.TabIndex = 0;
            
            // btnAddOrder
            this.btnAddOrder.Location = new System.Drawing.Point(12, 10);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(120, 30);
            this.btnAddOrder.TabIndex = 0;
            this.btnAddOrder.Text = "Добавить заказ";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            this.btnAddOrder.Click += new System.EventHandler(this.BtnAddOrder_Click);
            
            // flpOrders
            this.flpOrders.AutoScroll = true;
            this.flpOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOrders.Location = new System.Drawing.Point(0, 50);
            this.flpOrders.Name = "flpOrders";
            this.flpOrders.Size = new System.Drawing.Size(784, 411);
            this.flpOrders.TabIndex = 1;
            
            // OrdersForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.flpOrders);
            this.Controls.Add(this.pnlTop);
            this.Name = "OrdersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заказы";
            this.Load += new System.EventHandler(this.OrdersForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.FlowLayoutPanel flpOrders;
    }
}
