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
    public class PublicationLevel2ListModel : PagerModel
    {
		public string Search { get; set; }

		public string ParentId { get; set; }

		public List<PublicationLevel2Model> Data { get; set; }
    }

    public class PublicationLevel2Model : EditModePage
    {
		[Display(Name = "編號")]
		[Required(ErrorMessage="必填")]
		public string Id { get; set; }

		[Display(Name = "主類別編號")]
		[Required(ErrorMessage="必填")]
		public string Parentid { get; set; }
		public List<SelectListItem> ParentSelectList { get; set; }
		//public List<string> RoleList { get; set; }

		[Display(Name = "主類別名稱")]
		public string ParentName { get; set; }

		[Display(Name = "名稱")]
		[Required(ErrorMessage="必填")]
		public string Title { get; set; }
		public string ENTitle { get; set; }
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

        public PublicationLevel3ListModel Level3List { get; set; }

        public PublicationLevel2Model()
        {
			Id = Utils.GetObjectId();
			Createdate = DateTime.Now;

        }
    }
}