using System;
using System.Windows.Forms;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class OrderCard : UserControl
    {
        public Order OrderData { get; private set; }

        public OrderCard()
        {
            InitializeComponent();
        }

        public void BindData(Order order)
        {
            OrderData = order;
            lblArticle.Text = order.OrderArticle;
            lblStatus.Text = order.StatusName;
            lblPickupPoint.Text = order.PickupPointAddress;
            lblOrderDate.Text = $"Дата заказа: {order.OrderDate.ToShortDateString()}";
            lblDeliveryDate.Text = $"Дата доставки: {order.DeliveryDate.ToShortDateString()}";
        }
    }
}
