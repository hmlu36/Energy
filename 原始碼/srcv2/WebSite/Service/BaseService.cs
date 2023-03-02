using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JamZoo.Project.WebSite.DbContext;

namespace JamZoo.Project.WebSite.Service
{
    public class BaseService
    {
        /// <summary>
        /// 檔案上傳
        /// </summary>
        public static string UPLOAD_FOLDER = "Upload";

        public Entities basedb;

        public BaseService()
        {
            basedb = new Entities();
        }
    }
}