namespace ShoeShop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlHeader = new Panel();
            pbLogo = new PictureBox();
            btnOrders = new Button();
            lblUserInfo = new Label();
            btnLogout = new Button();
            pnlFilters = new Panel();
            txtSearch = new TextBox();
            cmbSort = new ComboBox();
            cmbFilter = new ComboBox();
            btnAddProduct = new Button();
            flpProducts = new FlowLayoutPanel();
            pnlFooter = new Panel();
            lblCount = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnlFilters.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Lime;
            pnlHeader.Controls.Add(pbLogo);
            pnlHeader.Controls.Add(btnOrders);
            pnlHeader.Controls.Add(lblUserInfo);
            pnlHeader.Controls.Add(btnLogout);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(800, 50);
            pnlHeader.TabIndex = 0;
            // 
            // pbLogo
            // 
            pbLogo.BackgroundImage = (Image)resources.GetObject("pbLogo.BackgroundImage");
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(12, 5);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(40, 40);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 3;
            pbLogo.TabStop = false;
            pbLogo.Click += pbLogo_Click;
            // 
            // btnOrders
            // 
            btnOrders.Location = new Point(60, 10);
            btnOrders.Name = "btnOrders";
            btnOrders.Size = new Size(100, 30);
            btnOrders.TabIndex = 2;
            btnOrders.Text = "Заказы";
            btnOrders.UseVisualStyleBackColor = true;
            btnOrders.Click += BtnOrders_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Location = new Point(300, 18);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(72, 15);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "ФИО (Роль)";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(620, 10);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(160, 30);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Выйти из аккаунта";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.Lime;
            pnlFilters.Controls.Add(txtSearch);
            pnlFilters.Controls.Add(cmbSort);
            pnlFilters.Controls.Add(cmbFilter);
            pnlFilters.Controls.Add(btnAddProduct);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 50);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Size = new Size(800, 40);
            pnlFilters.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += Filters_Changed;
            // 
            // cmbSort
            // 
            cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSort.FormattingEnabled = true;
            cmbSort.Location = new Point(230, 8);
            cmbSort.Name = "cmbSort";
            cmbSort.Size = new Size(180, 23);
            cmbSort.TabIndex = 1;
            cmbSort.SelectedIndexChanged += Filters_Changed;
            // 
            // cmbFilter
            // 
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(430, 8);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(150, 23);
            cmbFilter.TabIndex = 2;
            cmbFilter.SelectedIndexChanged += Filters_Changed;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(600, 7);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(150, 25);
            btnAddProduct.TabIndex = 3;
            btnAddProduct.Text = "Добавить товар";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += BtnAddProduct_Click;
            // 
            // flpProducts
            // 
            flpProducts.AutoScroll = true;
            flpProducts.Dock = DockStyle.Fill;
            flpProducts.Location = new Point(0, 90);
            flpProducts.Name = "flpProducts";
            flpProducts.Size = new Size(800, 330);
            flpProducts.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(lblCount);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 420);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(800, 30);
            pnlFooter.TabIndex = 3;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new Point(12, 7);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(159, 15);
            lblCount.TabIndex = 0;
            lblCount.Text = "Отображено X из Y товаров";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flpProducts);
            Controls.Add(pnlFooter);
            Controls.Add(pnlFilters);
            Controls.Add(pnlHeader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(816, 489);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Каталог товаров";
            Load += MainForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.FlowLayoutPanel flpProducts;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblCount;
    }
}
