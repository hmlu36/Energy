using Energy.Models.ViewModels;
using Energy.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Energy.Controllers
{
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 資料庫查詢類型
        /// </summary>
        /// <returns></returns>
        [HttpGet("SearchPageSetting")]
        public List<DatabaseType> GetDatabaseTypeList()
        {
            return _databaseService.GetSearchPageSetting();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpGet("Query")]
        public string Query() { 
            return null;
        }
    }
}
