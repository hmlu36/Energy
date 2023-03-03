using Energy.Models.ViewModels;
using Energy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Energy.Controllers
{
    public class DatabaseController : GenericController
    {
        private readonly IDatabaseService _databaseService;
        public DatabaseController(ILogger<GenericController> logger, IDatabaseService databaseService) : base(logger)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 資料庫查詢類型
        /// </summary>
        /// <returns></returns>
        [HttpGet("SearchPageSetting")]
        public ResultModel<List<DatabaseType>> GetDatabaseTypeList()
        {
            ResultModel<List<DatabaseType>> result = new ResultModel<List<DatabaseType>>();
            try
            {
                result.Success = true;
                result.Data = _databaseService.GetSearchPageSetting();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + ex.StackTrace;
                _logger.LogError(ex.Message + ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet("Query")]
        public ResultModel<List<DatabaseType>> Query(DatabaseCriteria criteria)
        {
            ResultModel<List<DatabaseType>> result = new ResultModel<List<DatabaseType>>();
            if (int.Parse(criteria.End) < int.Parse(criteria.Start))
            {
                result.Message = "搜尋的結束日期必需大於開始日期";
            }
            return result;
        }
    }
}
