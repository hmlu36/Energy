using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class UpfileLog
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public DateTime? FileTime { get; set; }

    public string? FileUser { get; set; }
}
