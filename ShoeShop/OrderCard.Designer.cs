namespace ShoeShop
{
    partial class OrderCard
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
            this.lblArticle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPickupPoint = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // lblArticle
            this.lblArticle.AutoSize = true;
            this.lblArticle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblArticle.Location = new System.Drawing.Point(10, 10);
            this.lblArticle.Name = "lblArticle";
            this.lblArticle.Size = new System.Drawing.Size(73, 21);
            this.lblArticle.TabIndex = 0;
            this.lblArticle.Text = "Артикул";
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(150, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Статус";
            
            // lblPickupPoint
            this.lblPickupPoint.AutoSize = true;
            this.lblPickupPoint.Location = new System.Drawing.Point(300, 15);
            this.lblPickupPoint.Name = "lblPickupPoint";
            this.lblPickupPoint.Size = new System.Drawing.Size(86, 15);
            this.lblPickupPoint.TabIndex = 2;
            this.lblPickupPoint.Text = "Пункт выдачи";
            
            // lblOrderDate
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(10, 45);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(75, 15);
            this.lblOrderDate.TabIndex = 3;
            this.lblOrderDate.Text = "Дата заказа:";
            
            // lblDeliveryDate
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Location = new System.Drawing.Point(300, 45);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(89, 15);
            this.lblDeliveryDate.TabIndex = 4;
            this.lblDeliveryDate.Text = "Дата доставки:";
            
            // OrderCard
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblDeliveryDate);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.lblPickupPoint);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblArticle);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "OrderCard";
            this.Size = new System.Drawing.Size(700, 80);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblArticle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPickupPoint;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDeliveryDate;
    }
}
