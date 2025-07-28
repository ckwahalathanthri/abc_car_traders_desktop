namespace ABCCarTraders.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ItemName { get; set; }
    }

    public enum ItemType
    {
        Car,
        Part
    }
}