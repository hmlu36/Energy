using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TRolePermissionMapping
{
    public string Id { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string PermissionId { get; set; } = null!;

    public string CreateUserId { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}
