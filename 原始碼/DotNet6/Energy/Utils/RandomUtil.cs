namespace Energy.Utils
{
    public class RandomUtil
    {
        // 取亂數名稱
        public static string GetRandomValue()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return rand.Next().ToString();
        }
    }
}
