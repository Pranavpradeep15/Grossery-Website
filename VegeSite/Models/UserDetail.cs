using System;
using System.Collections.Generic;

namespace VegeSite.Models;

public partial class UserDetail
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<Vegetable> Vegetables { get; set; } = new List<Vegetable>();
}
