using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public string ProductCategory { get; set; } = null!;
        public string Status { get; set; } = null!;
        [JsonIgnore]
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
