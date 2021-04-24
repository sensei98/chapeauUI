namespace Model
{
    public class OrderItem
    {
        public MenuItem item { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get { return item.Price * Quantity; } }
        public string Comment { get; set; }
        public ItemState State { get; set; }
    }
    public enum ItemState { Ready = 1, Preparing}
}
