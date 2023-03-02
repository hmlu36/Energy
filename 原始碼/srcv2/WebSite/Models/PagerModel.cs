﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

using JamZoo.Project.WebSite.Enums;
using System.IO;

namespace JamZoo.Project.WebSite.Models
{
    /// <summary>
    /// 詳細頁面
    /// </summary>
    public class EditModePage
    {
        [ScriptIgnore]
        public string UploadFolderName = "upload";

        /// <summary>
        /// 詳細頁面的模式
        /// </summary>
        [ScriptIgnore]
        public EditPageMode Mode { get; set; }
        /// <summary>
        /// 前一頁列表頁的頁數 (有需要的話)
        /// </summary>
        [ScriptIgnore]
        public int page { get; set; }

        /// <summary>
        /// 回上一頁的
        /// </summary>
        [ScriptIgnore]
        public string ReturnUrl { get; set; }

        public EditModePage()
        {
            page = 1;
        }
    }

    /// <summary>
    /// 分頁模式
    /// </summary>
    public class PagerModel
    {
        public int TotalRecords { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }
        //public int cPage { get; set; }

        public int Skip
        {
            get
            {
                if (Page <= 1)
                {
                    return 0;
                }
                else
                {
                    return (Page - 1) * PageSize;
                }
            }
        }

        public int Take
        {
            get
            {
                return PageSize;
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / (double)PageSize);
            }
        }

        public PagerModel()
        {
            PageSize = 50;
            Page = 1;
        }
    }
}