using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Labb2_REST_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductCategory { get; set; } = null!;

    public string Status { get; set; } = null!;
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
