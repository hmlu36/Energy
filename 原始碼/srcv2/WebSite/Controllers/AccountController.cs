using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Data;
using System.Globalization;

using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.Service;
using JamZoo.Project.WebSite.ViewModels;
using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Library.Principal;
using JamZoo.Project.WebSite.Library;
using JamZoo.Project.WebSite.Mvc;

namespace JamZoo.Project.WebSite.Controllers
{
    using DbContext;
    using NPOI.HSSF.UserModel;

    public class AccountController : BaseController
    {
        AccountService Service;
        [AllowAnonymous]
        public ActionResult Init()
        {
            string errMsg = "OK";

            
            Service.InitAdmin(out errMsg);
            Service.InitRole();
            Service.InitPermission();



            return Content(errMsg);
        }

        public AccountController()
        {
            Service = new AccountService();
        }

        [HttpPost]
        public ActionResult Test2(HttpPostedFileBase f, string File_Type = "")
        {

            DbContext.Entities db = new DbContext.Entities();

            if (File_Type != "")
            {
                using (DbContext.Entities db2 = new DbContext.Entities())
                {
                    upfile_log newRow = new upfile_log();
                    newRow.File_User = User.Identity.Name;
                    newRow.File_Name = f.FileName;
                    newRow.File_Type = File_Type;
                    newRow.File_Time = DateTime.Now;
                    db2.upfile_log.Add(newRow);
                    db2.SaveChanges();
                }

            }
            Stream ExcelFileStream = f.InputStream;
            String FileName = f.FileName;
            FileName = FileName.Replace(".xls", "");
            FileName = FileName.Substring(2, FileName.Length - 2);
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            int sheetIdx = 0;
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(sheetIdx);
            bool HASNEXT = true;
            while (HASNEXT)
            {
                String SheetName = sheet.SheetName;
                
                HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                }

                int rowCount = sheet.LastRowNum + 1;

                
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    HSSFRow row = (HSSFRow)sheet.GetRow(i);
                    if (row == null) break;
                    if (row.FirstCellNum == -1) break;

                    string YEAR = "0";
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (j == 0)
                        {
                            
                            YEAR = row.GetCell(j).ToString();
                            YEAR = YEAR.Replace("民國", "");
                            YEAR = YEAR.Replace("年", "");
                            YEAR = YEAR.Trim();
                            if (String.IsNullOrEmpty(YEAR)) break;
                        }
                        else
                        {
                            if (YEAR == "0") break;
                            string columnName = (headerRow.GetCell(j).StringCellValue);
                            var cell = row.GetCell(j);
                            if (cell == null) continue;
                            string VALUE = cell.ToString();
                           
                            if (columnName != "合計" && !columnName.Contains("單位"))         
                            {
                                T_ChartData data = new T_ChartData();
                                data.Page = FileName;
                                data.Year = Int32.Parse(YEAR);
                                data.Chart = SheetName;
                                data.ColumnName = columnName;
                                if (String.IsNullOrEmpty(VALUE))
                                {
                                    data.Value = (double)0.0;
                                } 
                                else
                                {
                                    data.Value = double.Parse(VALUE);
                                }
                                T_ChartData dbExist = db.T_ChartData.Where(p =>
                                    p.Page == data.Page &&
                                    p.Year == data.Year &&
                                    p.Chart == data.Chart &&
                                    p.ColumnName == data.ColumnName).FirstOrDefault();

                                if (dbExist != null)
                                {
                                    dbExist.Value = data.Value;
                                } 
                                else
                                {
                                    db.T_ChartData.Add(data);
                                }
                               
                                db.SaveChanges();
                            }
                        }
                    }
                }

                sheetIdx++;

                try
                {
                    sheet = (HSSFSheet)workbook.GetSheetAt(sheetIdx);
                }
                catch
                {
                    HASNEXT = false;
                }
            }

            ExcelFileStream.Close();
            GetFileLog();
            return Content("OK");
        }

        [HttpPost]
        public ActionResult Test(HttpPostedFileBase f, string dbName = "wesdes50",string File_Type="")
        {
            DataTable dt = DataTableRenderToExcel.RenderDataTableFromExcel(f.InputStream, 0, 0);
            if (File_Type != "")
            {
                using (DbContext.Entities db = new DbContext.Entities())
                {
                    upfile_log newRow = new upfile_log();
                    newRow.File_User = User.Identity.Name;
                    newRow.File_Name = f.FileName;
                    newRow.File_Type = File_Type;
                    newRow.File_Time = DateTime.Now;
                    db.upfile_log.Add(newRow);
                    db.SaveChanges();
                }

            }
            
            
            int noOfRowInserted = 0;

            using (DbContext.Entities db = new DbContext.Entities())
            {
                
                noOfRowInserted = ConvertToSQLScript(db, dt, dbName);
            }
            TempData["noOfRowInserted"] = noOfRowInserted;

            if (dbName== "wesdes50") 
            {
                dbName = dbName + "_db";
                using (DbContext.Entities db = new DbContext.Entities())
                {
                    
                    noOfRowInserted = ConvertToSQLScript(db, dt, dbName);
                }
                TempData["noOfRowInserted"] = noOfRowInserted;
            }

            using (DbContext.Entities db = new DbContext.Entities())
            {
                db.Database.ExecuteSqlCommand("truncate table[wesdes50_db]");
                db.Database.ExecuteSqlCommand("insert into[wesdes50_db] select * from[wesdes50] ");

            }


            
   
            GetFileLog();
            return View("Info");
        }

        [HttpPost]
        public ActionResult Test3(HttpPostedFileBase f, string File_Type = "")
        {
            DataTable dt =  DataTableRenderToExcel.RenderDataTableFromExcel(f.InputStream, 0, 0);
            if (File_Type != "")
            {
                using (DbContext.Entities db = new DbContext.Entities())
                {
                    upfile_log newRow = new upfile_log();
                    newRow.File_User = User.Identity.Name;
                    newRow.File_Name = f.FileName;
                    newRow.File_Type = File_Type;
                    newRow.File_Time = DateTime.Now;
                    db.upfile_log.Add(newRow);
                    db.SaveChanges();
                }

            }
            using (DbContext.Entities db = new DbContext.Entities())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM [dbo].[tmp_cc]");
            }

            int noOfRowInserted = 0;

            using (DbContext.Entities db = new DbContext.Entities())
            {
                
                noOfRowInserted = ConvertToSQLScript2(db, dt);
            }
            TempData["noOfRowInserted"] = noOfRowInserted;
            ToCoal50();
            GetFileLog();
            return View("Info");
        }


        [HttpGet]
        public ActionResult ToCoal50()
        {
            using (DbContext.Entities db = new DbContext.Entities())
            {
                var query = from p in db.tmp_cc
                            select p;

                foreach (tmp_cc row in query)
                {
                    string script = ConvertToCoal50Script(row);

                     db.Database.ExecuteSqlCommand(script);
   

                }

            }

            return Content("OK");
        }

        
        public static string ConvertToCoal50Script (tmp_cc row)
        {
            int row_no1 = 300 + int.Parse(row.code_1);
            string temp = @"
                UPDATE [dbo].[coal50]
                SET {0} = {1}
                WHERE [yr_mnth] = {2} AND [row_no1] = {3};
                    
                IF(@@ROWCOUNT = 0)
                BEGIN
                    INSERT INTO [dbo].[coal50] (yr_mnth, row_no1, {0}) VALUES({2}, {3}, {1});
                END;
            ";

            string query = string.Format(temp, row.code, row.amount, row.yr_mnth, row_no1.ToString());
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/Datalog.txt"), true))
            {
                sw.WriteLine(string.Format("{0}\t{1}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), query.Replace(Environment.NewLine, "")));
            }

            return query;
        }

        public static string convertColumnName(string Name)
        {
            if (Name == "年月") return "yr_mnth";
            else if (Name == "代碼") return "row_no1";

            foreach (var r in ColumnMapper.COLUMNS)
            {
                if (r.Value == Name) return r.Key;
            }

            return Name;
        }

        public static int ConvertToSQLScript2(DbContext.Entities db, DataTable dt, string dbName = "wesdes50")
        {

            int rows = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmp_cc newRow = new tmp_cc();
                newRow.yr_mnth = Int32.Parse(dt.Rows[i]["年月"].ToString());
                newRow.code = dt.Rows[i]["進口國代碼"].ToString();
                newRow.country = dt.Rows[i]["進口國"].ToString();
                string strAmount = dt.Rows[i]["數量"].ToString();
                newRow.amount = double.Parse(strAmount);
                newRow.code_1 = dt.Rows[i]["煤品別代碼"].ToString();
                newRow.code_name = dt.Rows[i]["煤品別"].ToString();
                db.tmp_cc.Add(newRow);
                db.SaveChanges();
                rows++;
            }

            return rows;
        }


        public static int ConvertToSQLScript(DbContext.Entities db, DataTable dt, string dbName = "wesdes50")
        {
            
            string i1 = @"
                UPDATE [{5}]
                SET {2}
                WHERE [yr_mnth] = {3} AND [row_no1] = {4};
                    
                IF(@@ROWCOUNT = 0)
                BEGIN
                    INSERT INTO [{5}] ({0}) VALUES({1});
                END;
";

            List<string> c1 = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "行業別") continue;
                dt.Columns[i].ColumnName = convertColumnName(dt.Columns[i].ColumnName);
                c1.Add("[" + dt.Columns[i].ColumnName + "]");
            }

            int rows = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                List<string> c2 = new List<string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName == "行業別") continue;
                    var culture = CultureInfo.CreateSpecificCulture("en-US");
                    try
                    {
                        if (dt.Rows[i][j].ToString() == "")
                        {
                            c2.Add("NULL");
                        }
                        else
                        { 
                            var number = Double.Parse(dt.Rows[i][j].ToString(), culture);
                            
                            
                            c2.Add(number.ToString());
                            
                            
                            
                            
                        }
                    }
                    catch
                    {
                        throw new Exception("Rows[i][j]:" + i + "," + j);
                    }
                }

                
                string c4 = "";
                string c5 = "";

                
                List<string> c3 = new List<string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName == "行業別") continue;
                    if (dt.Columns[j].ColumnName == "yr_mnth")
                    {
                        c4 = dt.Rows[i][j].ToString();
                        continue;
                    }
                    if (dt.Columns[j].ColumnName == "row_no1")
                    {
                        c5 = dt.Rows[i][j].ToString();
                        continue;
                    }
                    
                    var culture = CultureInfo.CreateSpecificCulture("en-US");
                    Double number = 0;

                    if (dt.Rows[i][j].ToString() != "")
                    {
                        number = Double.Parse(dt.Rows[i][j].ToString(), culture);
                        c3.Add("[" + dt.Columns[j].ColumnName + "]=" + number.ToString());
                    }
                    else
                    {
                        c3.Add("[" + dt.Columns[j].ColumnName + "]= NULL");
                    }
                }

                string s = string.Format(i1, string.Join(",", c1), string.Join(",", c2), string.Join(",", c3), c4, c5, dbName);

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/Datalog.txt"), true))
                {
                    sw.WriteLine(string.Format("{0}\t{1}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), s.Replace(Environment.NewLine, "")));
                }

                db.Database.ExecuteSqlCommand(s);

                
                db.Database.ExecuteSqlCommand("update [power50_db] set toag = mag+tag+aag");
                db.Database.ExecuteSqlCommand("update [power50] set toag_inst = mag_inst + aag_inst + tag_inst");
                rows++;
            }

            return rows;
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = convertColumnName(dt.Columns[i].ColumnName);
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            }
            html += "</tr>";
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        #region CRUD
        
        [Authorize]
        public ActionResult List(AccountListModel criteria, string download)
        {
            try
            {
                if (!string.IsNullOrEmpty(download) && download.Equals("匯出"))
                {
                    criteria.PageSize = 9999999;
                    AccountListModel model = Service.GetList(User.Identity.Name, criteria);
                    return new CsvResult(model.Data, "帳號匯出");
                }
                else
                {
                    AccountListModel model = Service.GetList(User.Identity.Name, criteria);
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }

            return View(criteria);
        }

        [Authorize]
        public ActionResult Add(int page = 1)
        {
            AccountModel entity = Service.NewInstance();
            entity.RoleSelectList = Service.GetRoleList("");
            entity.page = page;
            entity.Mode = EditPageMode.Create;
            return View(entity);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ErrMsgs = string.Empty;

                    if (Service.Create(
                        User.Identity.Name, 
                        model, out ErrMsgs))
                    {
                        return RedirectToAction("List", new { page = model.page });
                    }

                    ModelState.AddModelError("message", ErrMsgs);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("message", ex.Message);
                }
            }

            model.Mode = EditPageMode.Create;
            return View("Add", model);
        }


        [Authorize]
        public ActionResult Edit(string id, int page = 1)
        {
            AccountModel model = Service.Get(User.Identity.Name, id);
            model.Mode = EditPageMode.Update;
            model.RoleSelectList = Service.GetRoleList(model.RoleList != null ? model.RoleList.FirstOrDefault() : string.Empty);
            model.page = page;
            return View("Add", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ErrMsgs = string.Empty;

                    if (Service.Update(User.Identity.Name, model, out ErrMsgs))
                    {
                        return RedirectToAction("List", new { page = model.page });
                    }
                    else
                    {
                        ModelState.AddModelError("message", "修改失敗:" + ErrMsgs);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("message", ex.Message);
                }
            }

            model.Mode = EditPageMode.Update;
            return View("Add", model);
        }

        [Authorize]
        public ActionResult Delete(string id, int page = 1)
        {
            try
            {
                string errorMsg = string.Empty;

                if (Service.Delete(User.Identity.Name, id, out errorMsg))
                {
                    TempData["delete"] = true;
                }
                return RedirectToAction("List", new { page = page });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }
            return RedirectToAction("List");
        }
        #endregion

        public void GetFileLog()
        {
            using (DbContext.Entities db = new DbContext.Entities())
            {

                upfile_log dbExist = db.upfile_log.Where(p =>
                p.File_Type == "1-1").OrderByDescending(x=>x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File11 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "1-2").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File12 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "1-3").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File13 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-1").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File21 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-2").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File22 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-3").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File23 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-4").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File24 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-6").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File26 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "2-7").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File27 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "3-1").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File31 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "3-2").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File32 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "3-3").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File33 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "3-4").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File34 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "4-1").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File41 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "5-1").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File51 = dbExist.File_Time; }

                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "6-1").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File61 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "6-2").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File62 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "6-3").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File63 = dbExist.File_Time; }
                dbExist = db.upfile_log.Where(p =>
                p.File_Type == "6-4").OrderByDescending(x => x.File_Time).FirstOrDefault();
                if (dbExist != null) { ViewBag.File64 = dbExist.File_Time; }

            }
        }
        public ActionResult Info()
        {
            GetFileLog();
            return View();
        }

        #region 認證模組

        
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            
            LoginPage Model = new LoginPage();
            Model.ReturnUrl = returnUrl;

            return View("Index", Model);
        }

        
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginPage Model)
        {
            string errorMsg = string.Empty;
            if (ModelState.IsValid)
            {
                AccountModel user = null;
                bool isSuccess = Service.Authentication(
                    Model.Account, 
                    Model.P1,
                    out user,
                    out errorMsg);

                if (true == isSuccess)
                {
                    
                    WebSiteUser userData = new WebSiteUser()
                    {
                        Id = user.Id,
                        Account = user.Account,
                        Roles = user.RoleList.Select(p => p.ToString()).ToArray()
                    };

                    bool isCookiePersistent = false;
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string jsonAccountModel = serializer.Serialize(userData);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                              user.Id, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, jsonAccountModel);

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    if (true == isCookiePersistent)
                        authCookie.Expires = authTicket.Expiration;
                    Response.Cookies.Add(authCookie);
                    
                    if (!string.IsNullOrEmpty(Model.ReturnUrl))
                    {
                        Response.Redirect("~/Account/Info");
                    }
                    else
                    {
                        Response.Redirect("~/Account/Info");
                    }
                }

            }

            ModelState.AddModelError("message", errorMsg);

            return View("index", Model);
        }


        
        
        [AllowAnonymous]
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/account/info");
        }

        #endregion
        

    }
}

