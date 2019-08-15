using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Login.QQ
{
    public class QQTools
    {
        public static bool CheckUserInfo(string accessToken, string openid, string appid)
        {
            string str = "https://graph.qq.com/oauth2.0/me?access_token=" + accessToken;
            string rs = new YJYSoft.YL.Common.MyHttp().GetUrl(str);
            rs = rs.Replace("callback(", "");
            rs = rs.Replace(")", "");
            rs = rs.Replace(";", "");

            QQOpenIdInfo me = Newtonsoft.Json.JsonConvert.DeserializeObject<QQOpenIdInfo>(rs);

            if (me != null)
            {
                if (me.error == null)
                {
                    if (me.openid == openid && me.client_id == appid)
                        return true;
                }
            }
            return false;
        }
        public static QQUserInfo GetUserInfo(string accessToken, string openid, string appid)
        {
            string url_s_openid = string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}", accessToken, appid, openid);
            string str = new YJYSoft.YL.Common.MyHttp().GetUrl(url_s_openid);
            QQUserInfo n_qq_user_grxx = Newtonsoft.Json.JsonConvert.DeserializeObject<QQUserInfo>(str);

            if (n_qq_user_grxx != null)
            {
                if (n_qq_user_grxx.ret == 0)
                {
                    return n_qq_user_grxx;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
