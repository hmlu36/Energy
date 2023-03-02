using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TThematic
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// displayName=名稱;onList=true
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// displayName=圖檔;onList=true
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// displayName=內文;
    /// </summary>
    public string? Context { get; set; }

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
