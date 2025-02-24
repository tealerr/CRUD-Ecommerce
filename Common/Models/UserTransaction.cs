using System;
using System.Collections.Generic;

namespace Common.Models;

public partial class UserTransaction
{
    public int Id { get; set; }

    public string UserGuid { get; set; } = null!;

    public double GrandTotal { get; set; }

    public DateTime CreatedTime { get; set; }
}
