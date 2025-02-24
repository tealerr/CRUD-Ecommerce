using System;
using System.Collections.Generic;

namespace Common.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public string ImageUrl { get; set; } = null!;

    public sbyte IsDeleted { get; set; }

    public DateTime CreatedTime { get; set; }
}
