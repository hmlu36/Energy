using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TAccount
{
    public string Id { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string? Name { get; set; }

    public string Password { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public DateTime UpdateDate { get; set; }

    public string CreateUserId { get; set; } = null!;

    public string UpdateUserId { get; set; } = null!;

    public string? Ip { get; set; }
}
