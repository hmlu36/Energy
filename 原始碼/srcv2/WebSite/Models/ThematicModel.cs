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
    public class ThematicListModel : PagerModel
    {
		public string Search { get; set; }

		public List<ThematicModel> Data { get; set; }
    }

    public class ThematicModel : EditModePage
    {
		[Display(Name = "編號")]
		[Required(ErrorMessage="必填")]
		public string Id { get; set; }

		[Display(Name = "名稱")]
		public string Title { get; set; }

		[Display(Name = "圖檔")]
		public string Image { get; set; }

		public HttpPostedFileBase ImageFile { get; set; }

		[Display(Name = "內文")]
		public string Context { get; set; }

		[Display(Name = "建立時間")]
		[Required(ErrorMessage="必填")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime Createdate { get; set; }

		[Display(Name = "修改時間")]
		[DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime? Updatedate { get; set; }



        public ThematicModel()
        {
			Id = Utils.GetObjectId();
			Createdate = DateTime.Now;

        }
    }
}