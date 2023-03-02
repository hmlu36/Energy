﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JamZoo.Project.WebSite.Controllers.DatabaseRow
{
    using Models;
    using ViewModels;

    
    
    
    public class Page1 : Base
    {
        public void AddCell (DatabaseSearchPage viewModel, DataRow _row, DataColumn Header, EnergyModel EnergyRow, string CountryCode)
        {
            
            if ((string)_row["年季月"] == "M")
            {
                int s1 = int.Parse(Header.ColumnName);
                var mlist = viewModel.CoalData.Where(p => p.yr_mnth == s1 && EnergyRow.RowNo1Ary.Contains(p.row_no1)).ToList();

                foreach (var m in mlist)
                {
                    AddRow<DbContext.coal50>(_row, m, Header.ColumnName, CountryCode);
                }
            }
            else if ((string)_row["年季月"] == "Y")
            {
                string _only_y = Header.ColumnName;
                             List<int> yr_myth_list = new List<int>();
                for (int i = 0; i < 12; i++)
                {
                    int tmp = Int32.Parse(_only_y + ((i + 1).ToString("00")));
                    yr_myth_list.Add(tmp);
                }
                
                var mlist = viewModel.CoalData.Where(p => yr_myth_list.Contains(p.yr_mnth) && EnergyRow.RowNo1Ary.Contains(p.row_no1));

                foreach (var m in mlist)
                {
                    AddRow<DbContext.coal50>(_row, m, Header.ColumnName, CountryCode);
                }
            }
            
            else if ((string)_row["年季月"] == "Q")
            {
                string _only_q = Header.ColumnName;
                if (_only_q.Length == 4)
                {
                    _only_q = "0" + _only_q;
                }
                List<int> yr_myth_list = new List<int>();
                int Year = Int32.Parse(_only_q.Substring(0, 3));
                int Q = Int32.Parse(_only_q.Substring(4, 1));

                
                for (int i = 0; i < 3; i++)
                {
                    int M = Q * 3 - 2 + i;

                    int tmp = Int32.Parse(Year.ToString() + ((M).ToString("00")));
                    yr_myth_list.Add(tmp);
                }
                

                var mlist = viewModel.CoalData.Where(p => yr_myth_list.Contains(p.yr_mnth) && EnergyRow.RowNo1Ary.Contains(p.row_no1));
                foreach (var m in mlist)
                {
                    AddRow<DbContext.coal50>(_row, m, Header.ColumnName, CountryCode);
                }
            }
        }
    }
}
