﻿namespace Shared.OrderModels
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}