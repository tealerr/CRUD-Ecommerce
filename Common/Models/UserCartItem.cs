using System;
using System.Collections.Generic;

namespace Common.Models;

public partial class UserCartItem
{
    public int Id { get; set; }

    public string UserGuid { get; set; } = null!;

    public int ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public double Total { get; set; }
}
