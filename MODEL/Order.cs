using System;
using System.Collections.Generic;

namespace Model
{
    
    public class Order
    {
        
        public List<OrderItem> OrderItems { get; set; }
        public int OrderID { get; set; }
        public DateTime PurchaseTime { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal p = 0;
                foreach(OrderItem item in OrderItems)
                {
                    p += item.item.Price * item.Quantity;
                }
                return p;
            }
        }
        public OrderState State { get; set; }
        public Table Table { get; set; }
        public Employee Employee { get; set; }
        
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public bool Payment { get; set; }
        public decimal Total { get; set; }
        public decimal TotalVat { get; set; }
        public decimal Tip { get; set; }
    }
    public enum PaymentMethod { CreditCard = 1, Pin, Cash}
    public enum OrderState { Pending = 1, Served, Preparing, Ready } // add pending to db
}
