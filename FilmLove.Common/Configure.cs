using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Common
{
    public class Configure
    {
        /// <summary>
        /// IM AppKey
        /// </summary>
        public static string IMAppKey = GetAppString("IMAppKey", "cb384fdf89c9c9f3f1ba0d0e568eee31");
        /// <summary>
        /// IM AppSecret
        /// </summary>
        public static string IMAppSecret = GetAppString("IMAppSecret", "9c80134babab");
        /// <summary>
        /// IM RobotAccount 机器人账号ID
        /// </summary>
        public static string IMRobotAccount = GetAppString("IMRobotAccount", "-1");
        /// <summary>
        /// IM RobotToken 机器人token
        /// </summary>
        public static string IMRobotToken = GetAppString("IMRobotToken", "33e1f5c331fcb24f5484fc5a915c49c5");
        /// <summary>
        /// 是否验证主从
        /// </summary>
        public static int IsValZC = ConvertN.ToInt32(GetAppString("IsValZC", "0"));
        /// <summary>
        /// 是否验证游戏可玩
        /// </summary>
        public static int IsValGamePlay = ConvertN.ToInt32(GetAppString("IsValGamePlay", "0"));

        /// <summary>
        /// 
        /// </summary>
        public static string ImageUploadDomain = GetAppString("ImageUploadDomain", "");
        /// <summary>
        /// 
        /// </summary>
        public static string ImageReadDomain = GetAppString("ImageReadDomain", "");
        /// <summary>
        /// 上传文件保存路径
        /// </summary>
        public static string FileSavePath = GetAppString("FileSavePath", "");
        /// <summary>
        /// 上传文件响应域名
        /// </summary>
        public static string FileDomain = GetAppString("FileDomain", "");


        public static string GetAppString(string key, string defaultvalue = "")
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            return defaultvalue;
        }
        private static List<string> _TestAccount;
        public static List<string> TestAccount
        {
            get
            {
                if (_TestAccount == null)
                    _TestAccount = new List<string> {"14500000001","14500000002","14500000003","14500000004",
                        "14500000005","14500000006","14500000007","14500000008",
                        "14500000009","14500000010","14500000011","14500000012",
                        "14500000013","14500000014","14500000015","14500000016",
                        "14500000017","14500000018","14500000019","14500000020",

                        "14700000001","14700000002","14700000003","14700000004",
                        "14700000005","14700000006","14700000007","14700000008",
                        "14700000009","14700000010","14700000011","14700000012",
                        "14700000013","14700000014","14700000015","14700000016",
                        "14700000017","14700000018","14700000019","14700000020",
                        "14700000021","14700000022","14700000023","14700000024",
                        "14700000025","14700000026","14700000027","14700000028",
                        "14700000029","14700000030","14700000031","14700000032",
                        "14700000033","14700000034","14700000035","14700000036",
                        "14700000037","14700000038","14700000039","14700000040",
                        "18580701003"
                };
                return _TestAccount;
            }

        }
    }
}
