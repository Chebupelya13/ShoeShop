namespace ShoeShop
{
    partial class ProductCard
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
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblDiscountCost = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            
            // pbPhoto
            this.pbPhoto.Location = new System.Drawing.Point(10, 10);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(150, 100);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 0;
            this.pbPhoto.TabStop = false;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(170, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(126, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Категория | Наименование";
            
            // lblDescription
            this.lblDescription.Location = new System.Drawing.Point(170, 35);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(350, 35);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Описание";
            
            // lblManufacturer
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(170, 70);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(92, 15);
            this.lblManufacturer.TabIndex = 3;
            this.lblManufacturer.Text = "Производитель:";
            
            // lblSupplier
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(170, 90);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(73, 15);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Поставщик:";
            
            // lblCost
            this.lblCost.AutoSize = true;
            this.lblCost.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCost.Location = new System.Drawing.Point(540, 10);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(41, 19);
            this.lblCost.TabIndex = 4;
            this.lblCost.Text = "Цена";
            
            // lblDiscountCost
            this.lblDiscountCost.AutoSize = true;
            this.lblDiscountCost.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountCost.Location = new System.Drawing.Point(540, 30);
            this.lblDiscountCost.Name = "lblDiscountCost";
            this.lblDiscountCost.Size = new System.Drawing.Size(120, 19);
            this.lblDiscountCost.TabIndex = 5;
            this.lblDiscountCost.Text = "Цена со скидкой";
            
            // lblStock
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(540, 80);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(73, 15);
            this.lblStock.TabIndex = 6;
            this.lblStock.Text = "На складе: 0";
            
            // lblDiscount
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.Location = new System.Drawing.Point(680, 50);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(33, 21);
            this.lblDiscount.TabIndex = 7;
            this.lblDiscount.Text = "0%";
            
            // ProductCard
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblDiscountCost);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.lblManufacturer);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbPhoto);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProductCard";
            this.Size = new System.Drawing.Size(740, 155);
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblDiscountCost;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblDiscount;
    }
}
