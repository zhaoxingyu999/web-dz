using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Database.Enum
{
    public class EnumUserInfo
    {
        /// <summary>
        /// 账户状态0:启用1:禁用
        /// </summary>
        public enum DisState : int
        {
            /// <summary>
            /// 0 - 启用
            /// </summary>
            No = 0,
            /// <summary>
            /// 1 - 禁用
            /// </summary>
            Yes = 1

        }

        /// <summary>
        /// 用户基础信息
        /// 登陆枚举
        /// </summary>
        public enum IsLoginout : int
        {
            /// <summary>
            /// 0 - 未退出
            /// </summary>
            No = 0,
            /// <summary>
            /// 1 - 已退出
            /// </summary>
            Yes = 1

        }
    }
}
