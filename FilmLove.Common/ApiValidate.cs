using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace FilmLove.Common
{
    public class ApiValidate
    {
        /// <summary>
        /// 字典转字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private string DictToString(SortedDictionary<string, string> dict)
        {
            StringBuilder str = new StringBuilder();
            foreach(var s in dict)
            {
                str.Append(s.Key + s.Value);
            }
            return str.ToString();
        }
        /// <summary>
        /// 传入时间与服务器时间比较
        /// </summary>
        /// <param name="bybt">地址时间</param>
        /// <param name="time">head时间</param>
        /// <returns>相差大于20分钟，返回FALSE；否则返回true</returns>
        public bool CheckTime(string bybt,string time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = 0;
            if (long.TryParse(bybt + "0000", out lTime) == false)
                return false;
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime tm = dtStart.Add(toNow);
            //与headers 时间比较
            DateTime ht ;
            if (DateTime.TryParse(time, out ht) == false)
                return false;
            if (Math.Abs( (ht - tm).TotalSeconds) > 1)
                return false;
            //与系统时间比较
            if (Math.Abs((tm - DateTime.Now).TotalMinutes) > 20)
                return false;
            return true;
        }
        /// <summary>
        /// 访问验证
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public bool Validate(SortedDictionary<string,string> dict,string key,string md5)
        {
            string strmd5 = YJYSoft.YL.Common.Encrypt.MD5Encrypt(DictToString(dict)+key);
            return strmd5 == md5;
        }
        /// <summary>
        /// token 有sha256方式生产
        /// </summary>
        /// <returns></returns>
        public string TokenBySha256(SortedDictionary<string, string> dict,string key)
        {
            SHA256 sha256 = new SHA256Managed();
            //System.Diagnostics.Trace.WriteLine(DictToString(dict) + key);
            byte[] data = System.Text.Encoding.Default.GetBytes(DictToString(dict)+key);//将字符编码为一个字节序列 
            byte[] sha256data = sha256.ComputeHash(data);
            sha256.Clear();
            string str = "";
            for (int i = 0; i < sha256data.Length; i++)
            {
                str += sha256data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
    }
}
