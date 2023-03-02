using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TRole
{
    /// <summary>
    /// 編號
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// 群組名稱
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string CreateUserId { get; set; } = null!;

    public string UpdateUserId { get; set; } = null!;
}
