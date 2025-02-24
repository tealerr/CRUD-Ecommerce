using System;
using System.Collections.Generic;

namespace Common.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserGuid { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public DateTime CreatedTime { get; set; }
}
