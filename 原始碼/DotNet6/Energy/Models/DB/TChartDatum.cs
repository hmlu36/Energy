using System;
using System.Collections.Generic;

namespace Energy.Models.DB;

public partial class TChartDatum
{
    public string Page { get; set; } = null!;

    public int Year { get; set; }

    public string Chart { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public double? Value { get; set; }
}
