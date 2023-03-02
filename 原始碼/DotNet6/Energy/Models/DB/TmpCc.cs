using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TmpCc
{
    public int YrMnth { get; set; }

    public string Code { get; set; } = null!;

    public string? Country { get; set; }

    public string Code1 { get; set; } = null!;

    public string? CodeName { get; set; }

    public double? Amount { get; set; }
}
