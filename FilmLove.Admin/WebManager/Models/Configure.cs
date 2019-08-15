using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.WebManager.Models
{
    public class Configure
    {
        public static string ImageUploadDomain = GetAppString("ImageUploadDomain", "");
        public static string ImageReadDomain = GetAppString("ImageReadDomain", "");

        public static string GetAppString(string key, string defaultvalue = "")
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            return "";
        }
    }
}
