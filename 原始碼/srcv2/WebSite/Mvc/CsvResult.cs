using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamZoo.Project.WebSite.Mvc
{
    public class CsvResult : ActionResult
        
    {
        
        
        
        public string DownloadContent { get; private set; }

        public string FileName { get; private set; }

        public CsvResult(IEnumerable<object> Data, string FileName)
        {
            DownloadContent = Library.Utils.ToCsv<object, CsvFieldAttribute>(",", Data);
            this.FileName = FileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
            context.HttpContext.Response.Charset = "utf-8";
            context.HttpContext.Response.ContentType = "text/csv";
            context.HttpContext.Response.HeaderEncoding = System.Text.Encoding.UTF8;
            context.HttpContext.Response.Output.Write(DownloadContent);
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.End();
        }
    }
}
