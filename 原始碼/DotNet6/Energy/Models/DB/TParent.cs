using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TParent
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// displayName=名稱;onList=true
    /// </summary>
    public string EnergyName { get; set; } = null!;

    /// <summary>
    /// displayName=名稱2;onList=true
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// displayName=排序;onList=true
    /// </summary>
    public int Iorder { get; set; }

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    public string? Color { get; set; }

    public string? UnitList { get; set; }

    public string? HiddenChart { get; set; }

    public string? UnitListUpper { get; set; }
}
