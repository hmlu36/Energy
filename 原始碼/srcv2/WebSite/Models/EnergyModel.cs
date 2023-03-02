using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Library;

namespace JamZoo.Project.WebSite.Models
{
    public class EnergyListModel : PagerModel
    {
		public string Search { get; set; }

        public string[] SelectedValues { get; set; }


        public List<EnergyModel> Data { get; set; }
    }

    public class EnergyModel : EditModePage
    {
		[Display(Name = "編號")]
		[Required(ErrorMessage="必填")]
		public string Id { get; set; }

		[Display(Name = "頁面名稱")]
		public string Pagename { get; set; }

		[Display(Name = "父層編號")]
		public string Parentid { get; set; }

		[Display(Name = "深度")]
		[RegularExpression("\\d+", ErrorMessage="格式錯誤")]
		public int? Depth { get; set; }

		[Display(Name = "名稱")]
		public string Name { get; set; }

		[Display(Name = "表格名稱")]
		public string Tablename { get; set; }

		[Display(Name = "單位顯示上")]
		public string Unitlistupper { get; set; }

        public string[] UnitUpperAry
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Unitlistupper))
                {
                    return this.Unitlistupper.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[3];
                }
            }
        }

		[Display(Name = "單位顯示")]
		public string Unitlistbottom { get; set; }

        public string[] UnitBottomAry
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Unitlistbottom))
                {
                    return this.Unitlistbottom.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[3];
                }
            }
        }


        [Display(Name = "欄位編號")]
		public string Colidlist { get; set; }

        public string[] ColdIdAry
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Colidlist))
                {
                    return this.Colidlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[3];
                }
            }
        }

        [Display(Name = "備註")]
		public string Notes { get; set; }

		[Display(Name = "隱藏列表")]
		public string Hidelist { get; set; }
        public string[] HiddenChartList
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Hidelist))
                {
                    return this.Hidelist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[0];
                }
            }

        }
        public string HiddenChartListJsonString
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this.HiddenChartList);
            }

        }

        [Display(Name = "顏色編號列表")]
		public string Colorlist { get; set; }

        public string[] ColorArray
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Colorlist))
                {
                    return this.Colorlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[0];
                }
            }
        }

        public string ColorJsonString
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this.ColorArray);
            }
        }

        public int Iorder { get; set; }


        [Display(Name = "產業別編號")]
        [Required(ErrorMessage = "必填")]
        public string RowNo1 { get; set; }

        public int[] RowNo1Ary
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RowNo1))
                {
                    var temp = this.RowNo1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> returnList = new List<int>();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        int iii = 0;
                        if (int.TryParse(temp[i], out iii))
                        {
                            returnList.Add(iii);
                        }
                    }
                    return returnList.ToArray<int>();
                }
                else
                {
                    return new int[0];
                }
            }
        }



        public EnergyModel()
        {
			Id = Utils.GetObjectId();

        }
    }
}