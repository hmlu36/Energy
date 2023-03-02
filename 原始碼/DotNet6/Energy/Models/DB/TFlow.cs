using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TFlow
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// displayName=頁面名稱;onList=true
    /// </summary>
    public string? PageName { get; set; }

    /// <summary>
    /// displayName=父層編號;
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// displayName=深度;
    /// </summary>
    public int Depth { get; set; }

    /// <summary>
    /// displayName=名稱;
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// displayName=產業別編號;
    /// </summary>
    public string RowNo1 { get; set; } = null!;

    public int Iorder { get; set; }

    public string? ColIdList { get; set; }

    public int DecimalPlaces { get; set; }
}
