using System.Configuration;

namespace Cibb.Core.Cache
{
    public class CacheService
    {
        public static string GetConn(string name)
        {
            var key = name;
            var conn = CacheHelper.Get(key);
            if (conn != null)
                return conn.ToString();
            else
            {
                conn = ConfigurationManager.ConnectionStrings[name].ConnectionString;
                CacheHelper.Set(key, conn);
                return conn.ToString();
            }
        }
        public static T Get<T>()
        {
            var fileName = GetConfigFileName<T>();
            var content = CacheHelper.Get(fileName);
            return (T)content;
        }

        public static void Save<T>(T value)
        {
            var fileName = GetConfigFileName<T>();
            CacheHelper.Set(fileName, value);
        }

        public static string GetConfigFileName<T>()
        {
            var fileName = typeof(T).Name;
            return fileName;
        }

    }
}
