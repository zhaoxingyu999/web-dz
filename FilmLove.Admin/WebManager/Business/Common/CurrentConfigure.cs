using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Admin.ManagerBusiness.Common
{
    public class CurrentConfigure
    {
        /// <summary>
        /// 是否压缩输出
        /// </summary>
        public static int IsCompress = ConvertN.ToInt32(Configure.GetAppSettingKey("IsCompress", "0"));
    }
}
