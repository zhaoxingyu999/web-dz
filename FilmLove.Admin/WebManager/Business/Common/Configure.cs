using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.ManagerBusiness.Common
{
    public class Configure
    {
        /// <summary>
        /// 从配置文件中获取配置字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static string GetAppSettingKey(string key, string defaultvalue = "")
        {
            if (ConfigurationManager.AppSettings[key] == null)
                return defaultvalue;
            return ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 从配置中获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static string GetConnectionStringKey(string key, string defaultvalue = "")
        {
            if (ConfigurationManager.ConnectionStrings[key] == null)
                return defaultvalue;
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
