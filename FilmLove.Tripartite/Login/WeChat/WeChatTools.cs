using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Login.WeChat
{
    public class WeChatTools
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appsecret"></param>
        /// <returns></returns>
        public static string GetAccessToken(string appid, string appsecret)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + appsecret;
            string data = new YJYSoft.YL.Common.MyHttp().GetUrl(url);

            if (!string.IsNullOrWhiteSpace(data))
            {
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
                if (json.access_token != null && json.access_token != "")
                {
                    return json.access_token;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取OPENID
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="code_s"></param>
        /// <returns></returns>
        public static string GetOpenid(string appid, string secret, string code_s)
        {
            string url_s = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code_s + "&grant_type=authorization_code";

            string user_grxx = new YJYSoft.YL.Common.MyHttp().GetUrl(url_s);

            WeChatOpenIdInfo n_class_openId = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatOpenIdInfo>(user_grxx);
            if (n_class_openId.errcode == null)
            {
                return n_class_openId.openid;
            }
            return "";
        }

        public static WeChatUserInfo GetWXUserInfo(string accessToken, string openid)
        {
            string url_s_openid = "https://api.weixin.qq.com/sns/userinfo?access_token=" + accessToken + "&openid=" + openid;
            string str = new YJYSoft.YL.Common.MyHttp().GetUrl(url_s_openid);

            WeChatUserInfo n_wx_user_grxx = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatUserInfo>(str);

            if (n_wx_user_grxx != null)
            {
                if (n_wx_user_grxx.errcode == null)
                {
                    return n_wx_user_grxx;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public static WeChatUserInfo GetWXUserInfo2(string accessToken, string openid)
        {
            string url_s_openid = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + accessToken + "&openid=" + openid + "&lang=zh_CN";
            string str = new YJYSoft.YL.Common.MyHttp().GetUrl(url_s_openid);

            WeChatUserInfo n_wx_user_grxx = Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatUserInfo>(str);

            if (n_wx_user_grxx != null)
            {
                if (n_wx_user_grxx.errcode == null)
                {
                    return n_wx_user_grxx;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
