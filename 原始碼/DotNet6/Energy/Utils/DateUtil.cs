using System.Globalization;

namespace Energy.Utils
{
    public class DateUtil
    {
        /// <summary>
        /// 民國年轉西元年
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime ToAD(string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
            {
                return DateTime.MinValue;
            }
            return DateTime.Parse(dateStr, GetRocCulture());
        }

        // 定義民國年日曆的格式
        public static CultureInfo GetRocCulture()
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return culture;
        }
    }
}
