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
    public class PublicParentListModel : PagerModel
    {
        public string Id { get; set; }
        public string Pageid { get; set; }
        public string Parentid { get; set; }
		public string ParentName { get; set; }
		public string Title { get; set; }
        public string ENTitle { get; set; }
        public int Iorder { get; set; }
	    public DateTime? Updatedate { get; set; }
		public DateTime Createdate { get; set; }
        public List<PublicParentModel> Data { get; set; }
        
        public PublicParentListModel()
        {
            Id = Utils.GetObjectId();
            Createdate = DateTime.Now;
        }
    }

    public class PublicParentModel : EditModePage
    {
        public string ParentId { get; set; }
        public string Id { get; set; }
        public string Parentid { get; set; }
        public string ParentName { get; set; }
        public string Title { get; set; }
        public string ENTitle { get; set; }
        public int Iorder { get; set; }
        public DateTime? Updatedate { get; set; }
        public DateTime Createdate { get; set; }
        public List<PublicationLevel3Model> Data { get; set; }
        public PublicDetailListModel Child { get; set; }
        public PublicParentModel()
        {
            Id = Utils.GetObjectId();
            Createdate = DateTime.Now;

        }
    }

}