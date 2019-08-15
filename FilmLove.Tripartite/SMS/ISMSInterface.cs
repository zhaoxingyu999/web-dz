using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dualpsy.Tripartite.SMS
{
    /// <summary>
    /// 短信接口
    /// </summary>
    public interface ISMSInterface
    {
        int Send(string phone, SMSType type);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="Content"></param>
        /// <returns>0 表示成功</returns>
        int Send(string phone, string Content, SMSType type);
        /// <summary>
        /// 群发短信
        /// </summary>
        /// <param name="phones"></param>
        /// <param name="Content"></param>
        /// <returns>0 表示成功</returns>
        int BatchSend(List<string> phones, string Content);
        /// <summary>
        /// 获取返回结果的字符串
        /// </summary>
        /// <returns></returns>
        string GetMessageError();
    }
    public enum SMSType
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        CODE,
        /// <summary>
        /// 快捷注册成功
        /// </summary>
        SMS,
        /// <summary>
        /// 测试
        /// </summary>
        TEST,
        /// <summary>
        /// 共享商城修改密码
        /// </summary>
        ShareLoginPWDCode,
        /// <summary>
        /// 申请店长成功
        /// </summary>
        ApplyShopperSuccess,

    }
}
