using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TSystem
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// displayName=key;onList=true
    /// </summary>
    public string? Mk { get; set; }

    /// <summary>
    /// displayName=value;onList=true
    /// </summary>
    public string? Mv { get; set; }

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
