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
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    
    
    
    public class ColumnNameSort
    {
        public static Dictionary<string, int> SORT = new Dictionary<string, int>()
        {
            #region Sort Mapping
            { "能源供給結構(圓餅圖)煤及煤產品",   10101   },
            { "能源供給結構(圓餅圖)原油及石油產品", 10102   },
            { "能源供給結構(圓餅圖)天然氣", 10103   },
            { "能源供給結構(圓餅圖)核能",  10104   },
            { "能源供給結構(圓餅圖)再生能源",    10105   },
            { "能源供給量(長條圖)煤及煤產品",    10201   },
            { "能源供給量(長條圖)原油及石油產品",  10202   },
            { "能源供給量(長條圖)天然氣",  10203   },
            { "能源供給量(長條圖)核能",   10204   },
            { "能源供給量(長條圖)再生能源", 10205   },
            { "煤炭進口來源(長條圖)澳大利亞",    10301   },
            { "煤炭進口來源(長條圖)印尼",  10302   },
            { "煤炭進口來源(長條圖)俄羅斯", 10303   },
            { "煤炭進口來源(長條圖)南非",  10304   },
            { "煤炭進口來源(長條圖)加拿大", 10305   },
            { "煤炭進口來源(長條圖)中國大陸",    10306   },
            { "煤炭進口來源(長條圖)其他",  10307   },
            { "原油進口來源(長條圖)沙烏地阿拉伯",  10401   },
            { "原油進口來源(長條圖)科威特", 10402   },
            { "原油進口來源(長條圖)美國",  10403   },
            { "原油進口來源(長條圖)阿聯大公國",   10404   },
            { "原油進口來源(長條圖)伊拉克", 10405   },
            { "原油進口來源(長條圖)阿曼",  10406   },
            { "原油進口來源(長條圖)其他",  10407   },
            { "天然氣進口來源(長條圖)卡達", 10501   },
            { "天然氣進口來源(長條圖)馬來西亞",   10502   },
            { "天然氣進口來源(長條圖)澳大利亞",   10503   },
            { "天然氣進口來源(長條圖)俄羅斯",    10504   },
            { "天然氣進口來源(長條圖)印尼", 10505   },
            { "天然氣進口來源(長條圖)巴布亞紐幾內亞",    10506   },
            { "天然氣進口來源(長條圖)其他", 10507   },
            { "發電結構(圓餅圖)抽蓄水力",  20101   },
            { "發電結構(圓餅圖)燃煤",    20102   },
            { "發電結構(圓餅圖)燃油",    20103   },
            { "發電結構(圓餅圖)燃氣",    20104   },
            { "發電結構(圓餅圖)核能",    20105   },
            { "發電結構(圓餅圖)再生能源",  20106   },
            { "發電量(長條圖)抽蓄水力",   20201   },
            { "發電量(長條圖)燃煤", 20202   },
            { "發電量(長條圖)燃油", 20203   },
            { "發電量(長條圖)燃氣", 20204   },
            { "發電量(長條圖)核能", 20205   },
            { "發電量(長條圖)再生能源",   20206   },
            { "再生能源發電結構(圓餅圖)慣常水力",  20301   },
            { "再生能源發電結構(圓餅圖)地熱",    20302   },
            { "再生能源發電結構(圓餅圖)太陽光電",  20303   },
            { "再生能源發電結構(圓餅圖)風力",    20304   },
            { "再生能源發電結構(圓餅圖)生質能",   20305   },
            { "再生能源發電結構(圓餅圖)廢棄物",   20306   },
            { "再生能源發電量(長條圖)慣常水力",   20401   },
            { "再生能源發電量(長條圖)地熱", 20402   },
            { "再生能源發電量(長條圖)太陽光電",   20403   },
            { "再生能源發電量(長條圖)風力", 20404   },
            { "再生能源發電量(長條圖)生質能",    20405   },
            { "再生能源發電量(長條圖)廢棄物",    20406   },
            { "再生能源變動趨勢(折線圖)慣常水力",  20501   },
            { "再生能源變動趨勢(折線圖)地熱",    20502   },
            { "再生能源變動趨勢(折線圖)太陽光電",  20503   },
            { "再生能源變動趨勢(折線圖)風力",    20504   },
            { "再生能源變動趨勢(折線圖)生質能",   20505   },
            { "再生能源變動趨勢(折線圖)廢棄物",   20506   },
            { "發電裝置容量結構(圓餅圖)抽蓄水力",  20601   },
            { "發電裝置容量結構(圓餅圖)燃煤",    20602   },
            { "發電裝置容量結構(圓餅圖)燃油",    20603   },
            { "發電裝置容量結構(圓餅圖)燃氣",    20604   },
            { "發電裝置容量結構(圓餅圖)核能",    20605   },
            { "發電裝置容量結構(圓餅圖)再生能源",  20606   },
            { "發電裝置容量(長條圖)抽蓄水力",    20701   },
            { "發電裝置容量(長條圖)燃煤",  20702   },
            { "發電裝置容量(長條圖)燃油",  20703   },
            { "發電裝置容量(長條圖)燃氣",  20704   },
            { "發電裝置容量(長條圖)核能",  20705   },
            { "發電裝置容量(長條圖)再生能源",    20706   },
            { "再生能源發電裝置容量結構(圓餅圖)慣常水力",  20801   },
            { "再生能源發電裝置容量結構(圓餅圖)地熱",    20802   },
            { "再生能源發電裝置容量結構(圓餅圖)太陽光電",  20803   },
            { "再生能源發電裝置容量結構(圓餅圖)風力",    20804   },
            { "再生能源發電裝置容量結構(圓餅圖)生質能",   20805   },
            { "再生能源發電裝置容量結構(圓餅圖)廢棄物",   20806   },
            { "再生能源發電裝置容量(長條圖)慣常水力",    20901   },
            { "再生能源發電裝置容量(長條圖)地熱",  20902   },
            { "再生能源發電裝置容量(長條圖)太陽光電",    20903   },
            { "再生能源發電裝置容量(長條圖)風力",  20904   },
            { "再生能源發電裝置容量(長條圖)生質能", 20905   },
            { "再生能源發電裝置容量(長條圖)廢棄物", 20906   },
            { "電力備用容量率電力備用容量率", 21001   },
            { "電力備用容量率政府核定目標值", 21002   },
            { "能源別國內能源消費結構(圓餅圖)煤及煤產品",  30101   },
            { "能源別國內能源消費結構(圓餅圖)石油產品",   30102   },
            { "能源別國內能源消費結構(圓餅圖)天然氣",    30103   },
            { "能源別國內能源消費結構(圓餅圖)生質能及廢棄物",    30104   },
            { "能源別國內能源消費結構(圓餅圖)電力", 30105   },
            { "能源別國內能源消費結構(圓餅圖)太陽熱能",   30106   },
            { "能源別國內能源消費結構(圓餅圖)熱能", 30107   },
            { "能源別國內能源消費量(長條圖)煤及煤產品",   30201   },
            { "能源別國內能源消費量(長條圖)石油產品",    30202   },
            { "能源別國內能源消費量(長條圖)天然氣", 30203   },
            { "能源別國內能源消費量(長條圖)生質能及廢棄物", 30204   },
            { "能源別國內能源消費量(長條圖)電力",  30205   },
            { "能源別國內能源消費量(長條圖)太陽熱能",    30206   },
            { "能源別國內能源消費量(長條圖)熱能",  30207   },
            { "部門別國內能源消費結構(圓餅圖)能源部門自用", 30301   },
            { "部門別國內能源消費結構(圓餅圖)工業部門",   30302   },
            { "部門別國內能源消費結構(圓餅圖)運輸部門",   30303   },
            { "部門別國內能源消費結構(圓餅圖)農業部門",   30304   },
            { "部門別國內能源消費結構(圓餅圖)服務業部門",  30305   },
            { "部門別國內能源消費結構(圓餅圖)住宅部門",   30306   },
            { "部門別國內能源消費結構(圓餅圖)非能源消費",  30307   },
            { "部門別國內能源消費量(長條圖)能源部門自用",  30401   },
            { "部門別國內能源消費量(長條圖)工業部門",    30402   },
            { "部門別國內能源消費量(長條圖)運輸部門",    30403   },
            { "部門別國內能源消費量(長條圖)農業部門",    30404   },
            { "部門別國內能源消費量(長條圖)服務業部門",   30405   },
            { "部門別國內能源消費量(長條圖)住宅部門",    30406   },
            { "部門別國內能源消費量(長條圖)非能源消費",   30407   },
            { "電力消費結構(圓餅圖)能源部門自用",  40101   },
            { "電力消費結構(圓餅圖)工業部門",    40102   },
            { "電力消費結構(圓餅圖)運輸部門",    40103   },
            { "電力消費結構(圓餅圖)農業部門",    40104   },
            { "電力消費結構(圓餅圖)服務業部門",   40105   },
            { "電力消費結構(圓餅圖)住宅部門",    40106   },
            { "電力消費量(長條圖)能源部門自用",   40201   },
            { "電力消費量(長條圖)工業部門", 40202   },
            { "電力消費量(長條圖)運輸部門", 40203   },
            { "電力消費量(長條圖)農業部門", 40204   },
            { "電力消費量(長條圖)服務業部門",    40205   },
            { "電力消費量(長條圖)住宅部門", 40206   },
            { "能源生產力與密集度能源生產力(元/公升油當量)",    50101   },
            { "能源生產力與密集度能源密集度(公升油當量/千元)",   50102   },
            { "人均能源消費量與每人實質GDP平均每人能源消費量(公升油當量/人)",  50201   },
            { "人均能源消費量與每人實質GDP平均每人GDP(千元/人)",   50202   },
            { "人均能源消費量與用電量平均每人能源消費量(公升油當量/人)",  50301   },
            { "人均能源消費量與用電量平均每人用電量(度/人)",    50302   },
            { "能源依存度(折線圖)進口能源依存度",  50401   },
            { "能源依存度(折線圖)石油依存度",    50402   },
            { "能源集中度(折線圖)能源供應種類集中度",    50501   },
            { "能源集中度(折線圖)發電能源種類集中度",    50502   },
            { "平均每人負擔能源進口值(折線圖)平均每人負擔能源進口值",    50601   },
            { "進口能源總值(折線圖)煤炭",  50703   },
            { "進口能源總值(折線圖)原油",  50702   },
            { "進口能源總值(折線圖)液化天然氣", 50701   },
            { "電力排放係數(折線圖)電力排放係數",  50801   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)能源部門自用",  50901   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)工業部門",    50902   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)運輸部門",    50903   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)農業部門",    50904   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)服務業部門",   50905   },
            { "二氧化碳排放量(部門別含電-部門法)(長條圖)住宅部門",    50906   },
            { "二氧化碳排放量(燃料別-參考法)(長條圖)固體燃料",  51001   },
            { "二氧化碳排放量(燃料別-參考法)(長條圖)液體燃料",  51002   },
            { "二氧化碳排放量(燃料別-參考法)(長條圖)氣體燃料",  51003   },
            { "二氧化碳排放量(燃料別-參考法)(長條圖)廢棄物",   51004   },
            #endregion
        };
    }
    public class ThematicController : BaseWebController
    {
        ThematicService Service;


        public ThematicController()
        {
            Service = new ThematicService();
        }
        
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
        public ActionResult GetData(string datatype)
        {

            string certPath = HttpContext.Server.MapPath("~/cert.pfx");
            string certPass = "1234";

            X509Certificate2 prvcrt = new X509Certificate2(certPath,
                certPass, X509KeyStorageFlags.Exportable);
            RSACryptoServiceProvider prvkey = (RSACryptoServiceProvider)prvcrt.PrivateKey;
            
            byte[] encryptedData = Convert.FromBase64String("bzhNiUqcQYkaKz3E0VzrSHp4MnRryorbSQ4lZ/m9GIJEGYkfzWQN+lsn8/+dCSPbF3NrJ+boHXfDtolgG6T9YnpWtcN2lyu6b7yMyHeIWIe3g/OeipK3DlLCBaAoBhMZ6KyH/pjw5LylDYpecJ1H8f/4VnCKTkjU4/Hv9y/2Qy5PWsRgQ4ibD0xIN+Yd9RT7BdrLIeFNHbZ8F/FiGbcX9tvbkuuLN16tW3Ch/IjHBreaJcAH6L4Q+kHS3hl89ppKYBuJhdaBRhmigt86n7x6P3s67GjY2UkLsRRDwP3EVZ0pFm1YEtyYU+PEwlZXuFmNcg9mUx6y4qE55CL5q+riOg==");
            System.Security.Cryptography.RSAParameters parms = prvkey.ExportParameters(true);
            
            byte[] decryptedData = RSADecrypt(encryptedData, parms, false);
            

            return Content(Encoding.UTF8.GetString(decryptedData));

            
            
            
            
            

            
            
            

            
            
            
            
            
            
            
            
            
            
            
            
            

            
            
            
            

            SessionServicePortService service = new SessionServicePortService();
            getSessionRequest rq = new getSessionRequest();
            getSessionResponse rp = new getSessionResponse();

            try
            {
                rq.appid = "vtitnet"; 

                rp = service.getSession(rq); 
                string a = rp.encryptedToken;
                string b = rp.expiredTime;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Server.MapPath("~/log.txt"));
                
                sw.WriteLine(string.Format("{0} {1} : {2} ", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "encryptedToken", a));
                
                sw.WriteLine(string.Format("{0} {1} : {2} ", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "expiredTime", b));
                
                sw.Close();

            }
            catch (Exception e)
            {
                return Content(e.StackTrace + "<br>" + e.Message);
            }




        }
        
        public ActionResult Index(ThematicListModel criteria)
        {
            try
            {
                ThematicListModel model = Service.GetList(User.Identity.Name, criteria);

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", ex.Message);
            }
            return View(criteria);
        }

        
        public ChartViewModel calcPie(PageOfChartViewModel pv, int Year, string ChartId, Dictionary<string, string> config, List<T_ChartData> oriData)
        {
            ChartViewModel v = new ChartViewModel();

            v.Type = "pie";
            v.Id = ChartId;
            v.Title = Year + "年";
            if (oriData.First() != null)
            {
                v.Title += oriData.First().Chart;
            }

            v.SubTitle = config["subTitle"];

            oriData.Sort(new PieComparer());

            foreach (var row in oriData)
            {
                v.PieData[row.ColumnName] = row.Value.HasValue ? row.Value.Value : 0;
            }

            return v;
        }

        
        public ChartViewModel calcColumn(PageOfChartViewModel pv, string ChartName, Dictionary<string, string> config, List<T_ChartData> oriData, string chartType = "column")
        {
            ChartViewModel v = new ChartViewModel();

            v.Type = chartType;
            v.Id = config["chartId"];
            if (oriData.First() != null)
            {
                v.Title = oriData.First().Chart;
            }

            v.SubTitle = config["subTitle"];

            var colList = oriData.GroupBy(p => p.ColumnName).ToList();
            colList.Sort(new ColumnComparer(ChartName));

            v.CategoriesOfColumnData = new List<string>();
            for (int i = pv.Start; i <= pv.End; i++)
            {
                v.CategoriesOfColumnData.Add(i + "年");
            }

            
            foreach (var col in colList)
            {
                string _key = col.Key;
                List<double> values = new List<double>();

                var listValues = oriData.Where(p => p.ColumnName == _key).OrderBy(p => p.Year);

                foreach (var vv in listValues)
                {
                    values.Add(vv.Value.HasValue ? vv.Value.Value : 0);
                }


                v.ColumnData.Add(_key, values);
            }

            return v;
        }

        
        public ChartViewModel calcColumnLine(PageOfChartViewModel pv, Dictionary<string, string> config, List<T_ChartData> oriData, string chartType = "column")
        {
            ChartViewModel v = new ChartViewModel();

            v.Type = "columnLine";
            v.Id = config["chartId"];
            if (oriData.First() != null)
            {
                v.Title = oriData.First().Chart;
            }

            v.SubTitle = config["subTitle"];

            
            if (config.ContainsKey("htmlName"))
            {
                v.htmlName = config["htmlName"];
            }

            v.CategoriesOfColumnData = new List<string>();
            for (int i = pv.Start; i <= pv.End; i++)
            {
                v.CategoriesOfColumnData.Add(i + "年");
            }

            var colList = oriData.GroupBy(p => p.ColumnName).ToList();
            string[] columnNames = new string[2]
            {
             config["colNameLine"],
             config["colNameColumn"]
            };


            
            int x = 0;
            foreach (string col in columnNames)
            {
                string _key = col;
                List<double> values = new List<double>();

                var listValues = oriData.Where(p => p.ColumnName == _key).OrderBy(p => p.Year);

                foreach (var vv in listValues)
                {
                    values.Add(vv.Value.HasValue ? vv.Value.Value : 0);
                }

                if (x == 0)
                {
                    v.ColumnAName = _key;
                    v.ColumnAData = values;
                }
                else
                {
                    v.ColumnBName = _key;
                    v.ColumnBData = values;
                }
                x++;
            }

            return v;
        }


        
        public ActionResult Detail(int Id = 1, int Start = 104, int End = 108)
        {
            PageOfChartViewModel viewModel = new PageOfChartViewModel(Id, Start, End);
            using (DbContext.Entities db = new Entities())
            {
                var ListTChartData = db.T_ChartData.Where(p => p.Page == viewModel.PageName && (p.Year >= Start && p.Year <= End)).ToList();

                #region 電力供給 - 再生能源變動趨勢(折線圖)

                if (viewModel.PageName == "電力供給")
                {
                    const string CHART_NAME = "再生能源變動趨勢(折線圖)";
                    var ColumnGoupList = ListTChartData.Where(p => p.Chart == CHART_NAME).GroupBy(p => p.ColumnName).Select(p => p.Key);
                    foreach (string ColumnName in ColumnGoupList)
                    {
                        
                        
                        var DataList = ListTChartData.Where(p => p.Chart == CHART_NAME && p.ColumnName == ColumnName).OrderBy(p => p.Year).ToList();
                        if (DataList.Count() > 1 && (!DataList[0].Value.HasValue || DataList[0].Value.Value == 0))
                        {
                            DataList[0].Value = 0;
                            
                        }

                        double BaseValue = DataList[0].Value.Value;

                        
                        for (int i = 0; i < DataList.Count(); i++)
                        {
                            var Row = DataList[i];
                            if (!Row.Value.HasValue) continue;

                            
                            double newValue = 0.0f;
                            if (BaseValue > 0)
                            {
                                newValue = Row.Value.Value / BaseValue * 100;
                            }
                            int _Year = Row.Year;

                            var _first = ListTChartData.Where(p => p.Page == "電力供給" && p.Year == _Year && p.Chart == CHART_NAME && p.ColumnName == ColumnName).First();
                            foreach (var p in ListTChartData)
                            {
                                if (p.Page == "電力供給" && p.Year == _Year && p.Chart == CHART_NAME && p.ColumnName == ColumnName)
                                {
                                    
                                    p.Value = newValue;
                                }

                            }
                        }
                    }

                }

                #endregion

                
                var GrpChart = ListTChartData.GroupBy(p => p.Chart);

                foreach (var R1 in GrpChart)
                {
                    string ChartName = R1.Key;
                    
                    int iStart = ListTChartData.GroupBy(p => p.Year).OrderBy(p => p.Key).First().Key;
                    int iEnd = ListTChartData.GroupBy(p => p.Year).OrderByDescending(p => p.Key).First().Key;
                    if (!CHARTCONFIG.CONFIG.ContainsKey(viewModel.PageName + ChartName)) continue;
                    var _config = CHARTCONFIG.CONFIG[viewModel.PageName + ChartName];
                    if (_config["type"] == "pie")
                    {
                        var leftPie = ListTChartData.Where(p => p.Chart == ChartName && p.Year == iStart).ToList();
                        viewModel.ChartList.Add(calcPie(viewModel, iStart, _config["chartNameL"], _config, leftPie));
                        var rightPie = ListTChartData.Where(p => p.Chart == ChartName && p.Year == iEnd).ToList();
                        viewModel.ChartList.Add(calcPie(viewModel, iEnd, _config["chartNameR"], _config, rightPie));
                    }

                    if (_config["type"] == "column" || _config["type"] == "line")
                    {
                        var data = ListTChartData.Where(p => p.Chart == ChartName).OrderBy(p => p.Year).ToList();
                        var columnV = calcColumn(viewModel, ChartName, _config, data, _config["type"]);
                        viewModel.ChartList.Add(columnV);
                    }

                    if (_config["type"] == "columnLine")
                    {
                        var data = ListTChartData.Where(p => p.Chart == ChartName).OrderBy(p => p.Year).ToList();
                        var columnV = calcColumnLine(viewModel, _config, data, _config["type"]);
                        viewModel.ChartList.Add(columnV);
                    }
                }

            }

            return View(viewModel);
        }


     
    }

}

