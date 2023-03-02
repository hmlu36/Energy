using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TChild
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    public string? SelfId { get; set; }

    /// <summary>
    /// displayName=產業編號;onList=true
    /// </summary>
    public int IndustryId { get; set; }

    /// <summary>
    /// displayName=父層;onList=true
    /// </summary>
    public string ParentId { get; set; } = null!;

    /// <summary>
    /// displayName=名稱;onList=true
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// displayName=排序;onList=true
    /// </summary>
    public int Iorder { get; set; }

    /// <summary>
    /// displayName=所有產業別;onList=true
    /// </summary>
    public string? IndustryIds { get; set; }

    /// <summary>
    /// displayName=所有欄位編號;onList=true
    /// </summary>
    public string? ColumnIdAll { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId1 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId2 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId3 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId4 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId5 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId6 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId7 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId8 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId9 { get; set; }

    /// <summary>
    /// displayName=欄位名
    /// </summary>
    public string? ColumnId10 { get; set; }

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    public string? UnitList { get; set; }

    public string? AltName { get; set; }

    public int DecimalPlaces { get; set; }
}
