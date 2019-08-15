using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Database.Enum
{
    public class EnumSms
    {
        /// <summary>
        /// 131.注册，132.找回密码，151.修改登录密码，152.修改支付密码，153.绑定银行卡，162.短信登录-消费者注册登录
        /// </summary>
        public enum itype : int
        {
            /// <summary>
            ///131 - 注册【使用】
            /// </summary>
            Registered = 131,
            /// <summary>
            ///132 - 找回密码【使用】
            /// </summary>
            RetrievePassword = 132,
            /// <summary>
            ///133 - 绑定手机
            /// </summary>
            BingPhone = 133,
            /// <summary>
            /// 151 - 修改登录密码  
            /// </summary>
            ChangeLoginPasssword = 151,
            /// <summary>
            /// 152 - 修改支付密码
            /// </summary>
            ChangePayPassword = 152,
            /// <summary>
            /// 153 - 绑定银行卡
            /// </summary>
            BingBank = 153,
            /// <summary>
            ///162 - 短信登录-消费者注册登录
            /// </summary>
            RegLogin = 162,
        }
    }
}
