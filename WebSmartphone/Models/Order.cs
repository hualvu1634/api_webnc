using System;
using System.Collections.Generic;

namespace WebSmartphone.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public Status? OrdersStatus { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
