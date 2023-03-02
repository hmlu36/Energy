using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.ViewModels;
using JamZoo.Project.WebSite.Service;
using System.IO;

namespace JamZoo.Project.WebSite.Controllers
{
    public class HomeController : BaseWebController
    {
        PublicationService pubService;
        ThematicService theService;
        BannerService banService;

        public HomeController()
        {
            pubService = new PublicationService();
            theService = new ThematicService();
            banService = new BannerService();
        }

        
        
        
        
        
        
        
        
        
        

        public ActionResult plist(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcesses();
                List<string> rs = excelProcess.Select(p => p.ProcessName).ToList<string>();
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName(Name);
                List<string> rs = excelProcess.Select(p => p.ProcessName).ToList<string>();
                return Json(rs, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ToOds()
        {
            string fileName = @"C:\temp\2.xlsx";
            
            var excelAPP = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook book = excelAPP.Workbooks.Open(fileName);
            string odfPath = fileName.Replace("xlsx", "ods");
            book.SaveAs(odfPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenDocumentSpreadsheet);
            excelAPP.Visible = false;
            book.Close();
            excelAPP.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelAPP);
            excelAPP = null;
            GC.Collect();

            
            byte[] outPutFile = null;
            using (FileStream fs = new FileStream(odfPath, FileMode.Open))
            {
                outPutFile = new byte[fs.Length];
                fs.Read(outPutFile, 0, outPutFile.Length);
            }

            
            System.IO.File.Delete(fileName);
            
            System.IO.File.Delete(odfPath);

            return File(outPutFile, "application/vnd.oasis.opendocument.spreadsheet", odfPath);
        }

        
        

        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Url.Host))
            {
                string _l_host = Request.Url.Host.ToLower();
                if (_l_host == "www.esist.org.tw" || 
                    _l_host == "esist.org.tw" ||
                    _l_host == ""
                    )
                {
                    return Redirect("~/Database");

                }
            }

            IndexPage model = new IndexPage();
            return View(model);
        }

        public ActionResult Index2()
        {
            IndexPage model = new IndexPage();
            return View(model);
        }
        public ActionResult Privacy()
        {
            IndexPage model = new IndexPage();
            return View(model);
        }

    }
}

