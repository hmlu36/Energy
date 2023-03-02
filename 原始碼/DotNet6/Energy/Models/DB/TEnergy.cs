using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TEnergy
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
    public int? Depth { get; set; }

    /// <summary>
    /// displayName=名稱;
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// displayName=表格名稱;
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// displayName=單位顯示上;
    /// </summary>
    public string? UnitListUpper { get; set; }

    /// <summary>
    /// displayName=單位顯示;
    /// </summary>
    public string? UnitListBottom { get; set; }

    /// <summary>
    /// displayName=欄位編號;
    /// </summary>
    public string? ColIdList { get; set; }

    /// <summary>
    /// displayName=備註;
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// displayName=隱藏列表;
    /// </summary>
    public string? HideList { get; set; }

    /// <summary>
    /// displayName=顏色編號列表;
    /// </summary>
    public string? ColorList { get; set; }

    public int Iorder { get; set; }

    public string? RowNo1 { get; set; }
}
