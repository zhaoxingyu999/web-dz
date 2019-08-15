using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YJYSoft.YL.Common.Helper;

namespace FilmLove.Database.Enum
{
    public class EnumThirdAuth
    {
        public enum AuthType
        {
            /// <summary>
            /// 1 微信 
            /// </summary>
            [EnumAttribute("微信")]
            wx = 1,
            /// <summary>
            /// 2 QQ
            /// </summary>
            [EnumAttribute("QQ")]
            qq = 2,
            /// <summary>
            /// 3 微博
            /// </summary>
            [EnumAttribute("微博")]
            weibo = 3,
        }
    }
}
