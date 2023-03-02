using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TPublicationLevel3
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
    /// displayName=Ods;onList=true;fileType=image
    /// </summary>
    public string? Ods { get; set; }

    /// <summary>
    /// displayName=Pdf;onList=true;fileType=image
    /// </summary>
    public string? Pdf { get; set; }

    /// <summary>
    /// displayName=Word;onList=true;fileType=image
    /// </summary>
    public string? Word { get; set; }

    /// <summary>
    /// displayName=Meta;onList=true;fileType=image
    /// </summary>
    public string? Meta { get; set; }

    /// <summary>
    /// displayName=Excel;onList=true;fileType=image
    /// </summary>
    public string? Excel { get; set; }

    public string? Png { get; set; }

    /// <summary>
    /// displayName=建立時間;needToUpdate=false
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// displayName=修改時間;needToUpdate=true
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    public string Entitle { get; set; } = null!;

    public string? Json { get; set; }
}
