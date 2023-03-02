using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TIndustry
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// displayName=名稱;onList=true
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
