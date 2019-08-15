using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Tripartite.Login.WeChat
{
    public class WeChatUserInfo
    {
        /// <summary>
        /// 是否关注 1:关注
        /// </summary>
        public int subscribe { get; set; }

        public string openid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 性别 0：女 ，1：男；
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 语言:简体中文
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 特权
        /// </summary>
        public string[] privilege { get; set; }

        public long subscribe_time { get; set; }
        public string remark { get; set; }
        public int groupid { get; set; }
        public string subscribe_scene { get; set; }
        public int qr_scene { get; set; }
        public string qr_scene_str { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
