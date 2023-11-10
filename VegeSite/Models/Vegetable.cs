using System;
using System.Collections.Generic;

namespace VegeSite.Models;

public partial class Vegetable
{
    public int VegId { get; set; }

    public int? UserId { get; set; }

    public string? VegName { get; set; }

    public virtual UserDetail? User { get; set; }
}
