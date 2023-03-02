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
    //中類別 Lv2
    public class PublicDetailListModel : PagerModel
    {
        public string Id { get; set; }
		public string Parentid { get; set; }
		public string ParentName { get; set; }
		public string Title { get; set; }
		public string ENTitle { get; set; }
		public int Iorder { get; set; }
	    public DateTime? Updatedate { get; set; }
		public DateTime Createdate { get; set; }
        public List<PublicDetailModel> Data { get; set; }
        
        public PublicDetailListModel()
        {
            Id = Utils.GetObjectId();
            Createdate = DateTime.Now;
        }
    }

    public class PublicDetailModel : EditModePage
    {
        public string ParentId { get; set; }

		[Display(Name = "編號")]
		[Required(ErrorMessage = "必填")]
		public string Id { get; set; }

		[Display(Name = "子類別編號")]
		[Required(ErrorMessage = "必填")]
		public string Parentid { get; set; }

		[Display(Name = "名稱")]
		[Required(ErrorMessage = "必填")]
		public string Title { get; set; }
		public string EnTitle { get; set; }
		[Display(Name = "主類別名稱")]
		public string ParentName { get; set; }
		public List<SelectListItem> ParentSelectList { get; set; }

		[Display(Name = "排序")]
		[Required(ErrorMessage = "必填")]
		[RegularExpression("\\d+", ErrorMessage = "格式錯誤")]
		public int Iorder { get; set; }

		[Display(Name = "Ods")]
		public string Ods { get; set; }

		public HttpPostedFileBase OdsFile { get; set; }

		[Display(Name = "Pdf")]
		public string Pdf { get; set; }
		public HttpPostedFileBase PdfFile { get; set; }

		[Display(Name = "Word")]
		public string Word { get; set; }
		public HttpPostedFileBase WordFile { get; set; }

		[Display(Name = "Meta")]
		public string Meta { get; set; }
		public HttpPostedFileBase MetaFile { get; set; }

		[Display(Name = "Excel")]
		public string Excel { get; set; }
		public HttpPostedFileBase ExcelFile { get; set; }


		[Display(Name = "PNG")]
		public string PNG { get; set; }
		public HttpPostedFileBase PNGFile { get; set; }

		[Display(Name = "JSON")]
		public string JSON { get; set; }
		public HttpPostedFileBase JSONFile { get; set; }

		[Display(Name = "建立時間")]
		[Required(ErrorMessage = "必填")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Createdate { get; set; }

		[Display(Name = "修改時間")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime? Updatedate { get; set; }



		public List<PublicationLevel3Model> Data { get; set; }
        public PublicDetailListModel Child { get; set; }
        public PublicDetailModel()
        {
            Id = Utils.GetObjectId();
            Createdate = DateTime.Now;

        }
    }

}