﻿namespace StockControl.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
