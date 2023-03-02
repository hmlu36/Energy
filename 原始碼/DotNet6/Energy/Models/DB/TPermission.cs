using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TPermission
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string CreateUserId { get; set; } = null!;

    public string UpdateUserId { get; set; } = null!;

    public string Entitle { get; set; } = null!;
}
