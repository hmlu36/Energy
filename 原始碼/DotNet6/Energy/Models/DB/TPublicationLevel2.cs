using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TPublicationLevel2
{
    /// <summary>
    /// geekors=true;displayName=編號;isHidden=true
    /// </summary>
    public string Id { get; set; } = null!;

    public string ParentId { get; set; } = null!;

    /// <summary>
    /// displayName=名稱;onList=true
    /// </summary>
    public string Title { get; set; } = null!;

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

    public string Entitle { get; set; } = null!;
}
