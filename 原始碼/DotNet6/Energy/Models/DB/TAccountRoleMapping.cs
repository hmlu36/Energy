using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TAccountRoleMapping
{
    public string Id { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string CreateUserId { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}
