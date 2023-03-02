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
    public class ParentListModel : PagerModel
    {
		public string Search { get; set; }

        public string EnergyName { get; set; }

		public List<ParentModel> Data { get; set; }
    }

    public class ParentModel : EditModePage
    {
		[Display(Name = "編號")]
		[Required(ErrorMessage="必填")]
		public string Id { get; set; }

		[Display(Name = "名稱")]
		[Required(ErrorMessage="必填")]
		public string Energyname { get; set; }

		[Display(Name = "名稱2")]
		[Required(ErrorMessage="必填")]
		public string Name { get; set; }

		[Display(Name = "排序")]
		[Required(ErrorMessage="必填")]
		[RegularExpression("\\d+", ErrorMessage="格式錯誤")]
		public int Iorder { get; set; }

		[Display(Name = "建立時間")]
		[Required(ErrorMessage="必填")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Createdate { get; set; }

		[Display(Name = "修改時間")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime? Updatedate { get; set; }

        public string HiddenChart { get; set; }

        public string[] HiddenChartList
        {
            get
            {
                if (!string.IsNullOrEmpty(this.HiddenChart))
                {
                    return this.HiddenChart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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

        [Display(Name = "顏色名字")]
        public string Color { get; set; }

        public string[] ColorArray
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Color))
                {
                    return this.Color.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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

        public string UnitListUpper { get; set; }

        public string[] UnitListUpperArray
        {
            get
            {
                if (!String.IsNullOrEmpty(this.UnitListUpper))
                {
                    return this.UnitListUpper.Split(new char[] { ',' });
                }
                else
                {
                    return new string[3] { "", "", "" };
                }
            }
        }


        public string UnitList { get; set; }

        public string[] UnitListArray
        {
            get
            {
                if (!String.IsNullOrEmpty(this.UnitList))
                {
                    return this.UnitList.Split(new char[] { ',' });
                }
                else
                {
                    return new string[3] { "", "", "" };
                }
            }
        }

        public Dictionary<int, Dictionary<int, string>> UnitDict = new Dictionary<int, Dictionary<int, string>>() {
            {
                0,
                new Dictionary<int, string>() {
                    { 0, "無" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },

            {
                1,
                new Dictionary<int, string>() {
                    { 0, "公秉油當量" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },

            {
                2,
                new Dictionary<int, string>() {
                    { 0, "公秉油當量" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },

            {
                3,
                new Dictionary<int, string>() {
                    { 0, "公噸" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },

            {
                4,
                new Dictionary<int, string>() {
                    { 0, "無" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },
            {
                5,
                new Dictionary<int, string>() {
                    { 0, "公秉油當量" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },

            {
                6,
                new Dictionary<int, string>() {
                    { 0, "公秉油當量" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },
            {
                7,
                new Dictionary<int, string>() {
                    { 0, "公噸" },
                    { 1, "公噸油當量/107千卡" },
                    { 2, "公秉油當量" },
                }
            },
        };


        public ChildListModel Child { get; set; }

        public ParentModel()
        {
			Id = Utils.GetObjectId();
			Createdate = DateTime.Now;

        }
    }
}