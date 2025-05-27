﻿using System.Text.Json.Serialization;

namespace Shared
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
