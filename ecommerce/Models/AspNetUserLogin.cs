using System;
using System.Collections.Generic;

namespace ecommerce.Models;

public partial class AspNetUserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
