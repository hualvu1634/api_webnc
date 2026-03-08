using System;
using System.Collections.Generic;

namespace WebSmartphone.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Descriptions { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public int? Quantity { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? ProductDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
