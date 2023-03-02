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
    public class ChildListModel : PagerModel
    {
		public string Search { get; set; }

		public string ParentId { get; set; }

        public string SelfId { get; set; }

		public List<ChildModel> Data { get; set; }
    }

    public class ChildModel : EditModePage
    {
		[Display(Name = "編號")]
		[Required(ErrorMessage="必填")]
		public string Id { get; set; }

		[Display(Name = "產業編號")]
		[Required(ErrorMessage="必填")]
		[RegularExpression("\\d+", ErrorMessage="格式錯誤")]
		public int Industryid { get; set; }

		[Display(Name = "父層")]
		[Required(ErrorMessage="必填")]
		public string Parentid { get; set; }

        [Display(Name = "上一層")]
        [Required(ErrorMessage = "必填")]
        public string Selfid { get; set; }

        [Display(Name = "名稱")]
		[Required(ErrorMessage="必填")]
		public string Name { get; set; }

		[Display(Name = "排序")]
		[Required(ErrorMessage="必填")]
		[RegularExpression("\\d+", ErrorMessage="格式錯誤")]
		public int Iorder { get; set; }

		[Display(Name = "所有產業別")]
		public string Industryids { get; set; }

        [Display(Name = "小數點位數")]
        public int DecimalPlaces { get; set; }

        [Display(Name = "所有欄位編號")]
		public string Columnidall { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid1 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid2 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid3 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid4 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid5 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid6 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid7 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid8 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid9 { get; set; }

		[Display(Name = "欄位名")]
		public string Columnid10 { get; set; }

		[Display(Name = "建立時間")]
		[Required(ErrorMessage="必填")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Createdate { get; set; }

		[Display(Name = "修改時間")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime? Updatedate { get; set; }

        public ChildListModel ChildList { get; set; }

        public int TotalMonth { get; set; }

        public int CheckBoxIdx { get; set; }

        public string IndentName { get; set; }

        /// <summary>
        /// 縮排 depth
        /// </summary>
        public int IndentNum { get; set; }

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

        public string AltName { get; set; }

        public string[] AltNameArray
        {
            get
            {
                if (!String.IsNullOrEmpty(this.AltName))
                {
                    return this.AltName.Split(new char[] { ',' });
                }
                else
                {
                    return new string[3] { "", "", "" };
                }
            }
        }


        public ChildModel()
        {
			Id = Utils.GetObjectId();
			Createdate = DateTime.Now;

        }
    }
}