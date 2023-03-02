/*
ModelName
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using JamZoo.Project.WebSite.Models;
using JamZoo.Project.WebSite.Service;
using JamZoo.Project.WebSite.ViewModels;
using JamZoo.Project.WebSite.Enums;
using JamZoo.Project.WebSite.Library.Principal;

namespace JamZoo.Project.WebSite.Controllers
{
    using DbContext;
    using IComparer;
    using JamZoo.Project.WebSite.tw.org.iedrp;
    using JamZoo.Project.WebSite.tw.org.iedrprpt;
    using Newtonsoft.Json;
    using System.Data;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;


    public class JobController : BaseWebController
    {

        
        private const int _EncryptionKeySize = 2048;

        
        private const int _DecryptionBufferSize = (_EncryptionKeySize / 8);

        
        private const int _EncryptionBufferSize = _DecryptionBufferSize - 11;
        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                
                
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    
                    
                    RSA.ImportParameters(RSAKeyInfo);

                    
                    
                    
                    
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(DataToDecrypt.Length))
                    {

                        

                        byte[] buffer = new byte[_DecryptionBufferSize];

                        int pos = 0;

                        int copyLength = buffer.Length;

                        while (true)
                        {

                            

                            Array.Copy(DataToDecrypt, pos, buffer, 0, copyLength);

                            

                            pos += copyLength;

                            

                            
                            
                            

                            byte[] resp = RSA.Decrypt(buffer, false);

                            ms.Write(resp, 0, resp.Length);

                            

                            Array.Clear(resp, 0, resp.Length);

                            Array.Clear(buffer, 0, copyLength);

                            

                            if (pos >= DataToDecrypt.Length)

                                break;

                        }

                        

                        return ms.ToArray();

                    }
                }
                
            }
            
            
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

     

        public ActionResult GetData(string datatype,string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                
                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();
                getOECDDataRequest get_rq = new getOECDDataRequest();
                getOECDDataResponse get_rp = new getOECDDataResponse();
                string parameters = "<paramsOECDData xmlns=\"http://webServices.jcs/beans/report\">" +
                            "	<UNIT>"+ datatype +"</UNIT>" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsOECDData>";    

 

                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getOECDData(get_rq); 

               
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;

                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript(db, dt, "wesdes50");
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;                    

                    
                    
                    
                    

                    

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");

                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    

                    
                    
                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }
        }
        public ActionResult GetImportData(string datatype, string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();

                getImportDataRequest get_rq = new getImportDataRequest();
                getImportDataResponse get_rp = new getImportDataResponse();
                string parameters = "<paramsImportData xmlns=\"http://webServices.jcs/beans/report\">" +
                            "	<ENGTYPE>" + datatype + "</ENGTYPE>" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsImportData>";



                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getImportData(get_rq); 

             
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;
                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM [dbo].[tmp_cc]");
                    }


                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript2(db, dt);
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;

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

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");


                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }
        public ActionResult GetIndexData(string datatype, string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();
                getIndexDataRequest get_rq = new getIndexDataRequest();
                getIndexDataResponse get_rp = new getIndexDataResponse();
                string parameters = "<paramsIndexData xmlns=\"http://webServices.jcs/beans/report\">" +
                            "	<DATATYPE>" + datatype + "</DATATYPE>" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsIndexData>";

   

                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getIndexData(get_rq); 

               
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;

                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript(db, dt, "energy50_db");
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");

                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }
        public ActionResult GetPowerData(string datatype, string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                
                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();
                getPowerDataRequest get_rq = new getPowerDataRequest();
                getPowerDataResponse get_rp = new getPowerDataResponse();
                string parameters = "<paramsPowerData xmlns=\"http://webServices.jcs/beans/report\">" +
                            "" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsPowerData>";



                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getPowerData(get_rq); 

                
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;

                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript(db, dt, "power50_db");
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");

                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }
        public ActionResult GetDevCapData(string datatype, string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                
                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();
                getDevCapDataRequest get_rq = new getDevCapDataRequest();
                getDevCapDataResponse get_rp = new getDevCapDataResponse();
                string parameters = "<paramsDevCapData xmlns=\"http://webServices.jcs/beans/report\">" +
                            "" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsDevCapData >";


                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getDevCapData(get_rq); 

                
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;

                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript(db, dt, "power50");
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");

                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }
        public ActionResult GetFuelInputData(string datatype, string YYYMM1, string YYYMM2)
        {
            string TokenKey = "";

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;

                
                string certPath = HttpContext.Server.MapPath("~/cert.pfx");
                string certPass = "1234";

                X509Certificate2 prvcrt = new X509Certificate2(certPath,
                    certPass, X509KeyStorageFlags.Exportable);
                RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
                
                byte[] encryptedData = Convert.FromBase64String(a);
                System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
                
                byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
                

                TokenKey = Encoding.UTF8.GetString(decryptedData);


                ReportServicePortService report_service = new ReportServicePortService();
                getFuelInputDataRequest get_rq = new getFuelInputDataRequest();
                getFuelInputDataResponse get_rp = new getFuelInputDataResponse();
                string parameters = "<paramsFuelInputData  xmlns=\"http://webServices.jcs/beans/report\">" +
                            "	<DATATYPE>" + datatype + "</DATATYPE>" + 
                            "	<YYYMM1>" + YYYMM1 + "</YYYMM1>" + 
                            "	<YYYMM2>" + YYYMM2 + "</YYYMM2>" + 
                            "</paramsFuelInputData>";


                get_rq.appid = "vtitnet";
                get_rq.parameters = this.EncryptAES(parameters, TokenKey);

                

                get_rp = report_service.getFuelInputData(get_rq); 

                
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.DecryptAFS(get_rp.result, TokenKey));
                if (dt != null && dt.Rows.Count > 0)
                {


                    int noOfRowInserted = 0;

                    using (DbContext.Entities db = new DbContext.Entities())
                    {
                        noOfRowInserted = ConvertToSQLScript(db, dt, "fuel50_db");
                    }
                    TempData["noOfRowInserted"] = noOfRowInserted;

                    return Content("匯入成功，共取得" + dt.Rows.Count + "筆資料");

                }

                return Content("查無資料");

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }

        public static string ConvertToCoal50Script(tmp_cc row)
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


            return query;
        }
        public string EncryptAES(string OriginalString, string token) {
            string result = "";
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                byte[] bytes2 = Encoding.UTF8.GetBytes(OriginalString);
                string text = Convert.ToBase64String(bytes2);
                ICryptoTransform cryptoTransform = new RijndaelManaged
                {
                    Key = bytes,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    IV = Encoding.UTF8.GetBytes("88095688" + DateTime.Today.Date.ToString("yyyyMMdd"))
                }.CreateEncryptor();
                byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
                result = Convert.ToBase64String(array, 0, array.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public string DecryptAFS(string Originalstring, string token)
        {
            string result = "";
            try {
                byte[] bytes = Encoding.UTF8.GetBytes(token);
                byte[] array = Convert.FromBase64String(Originalstring);
                ICryptoTransform cryptoTransform = new RijndaelManaged
                {
                    Key = bytes,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    IV = Encoding.UTF8.GetBytes("88095688" + DateTime.Today.Date.ToString("yyyyMMdd"))
                }.CreateDecryptor();
                byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0 , array.Length);
                result = Encoding.UTF8.GetString(bytes2);
            } catch (Exception ex) {
                throw ex;
            }
        return result;
        }
        public static string convertColumnName(string Name)
        {
            if (Name == "yyymm") return "yr_mnth";
            else if (Name == "code") return "row_no1";

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
                newRow.yr_mnth = Int32.Parse(dt.Rows[i]["yr_mnth"].ToString());
                newRow.code = dt.Rows[i]["code"].ToString();
                newRow.country = dt.Rows[i]["country"].ToString();
                string strAmount = dt.Rows[i]["strAmount"].ToString();
                newRow.amount = double.Parse(strAmount);
                newRow.code_1 = dt.Rows[i]["code_1"].ToString();
                newRow.code_name = dt.Rows[i]["code_name"].ToString();
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
                if (dt.Columns[i].ColumnName == "industry") continue;
                dt.Columns[i].ColumnName = convertColumnName(dt.Columns[i].ColumnName);
                c1.Add("[" + dt.Columns[i].ColumnName + "]");
            }

            int rows = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                List<string> c2 = new List<string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName == "industry") continue;
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
                    if (dt.Columns[j].ColumnName == "industry") continue; 
                    if (dt.Columns[j].ColumnName == "yr_mnth" || dt.Columns[j].ColumnName == "yyymm")
                    {
                        c4 = dt.Rows[i][j].ToString();
                        continue;
                    }
                    if (dt.Columns[j].ColumnName == "row_no1" || dt.Columns[j].ColumnName == "code")
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



                db.Database.ExecuteSqlCommand(s);


                rows++;
            }
            
            db.Database.ExecuteSqlCommand("update [power50_db] set toag = mag+tag+aag");
            db.Database.ExecuteSqlCommand("update [power50] set toag_inst = mag_inst + aag_inst + tag_inst");
            return rows;
        }

        
        public ActionResult Index(ThematicListModel criteria)
        {
            return View();
        }

    }

}

