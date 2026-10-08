using System;

namespace ShoeShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal Cost { get; set; }
        public string Unit { get; set; }
        public int StockQuantity { get; set; }
        public int Discount { get; set; }
        public string PhotoPath { get; set; }

        public decimal DiscountedCost => Cost - (Cost * Discount / 100);
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderArticle { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int PickupPointId { get; set; }
        public string PickupPointAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Count { get; set; }
    }

    public class DictionaryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
