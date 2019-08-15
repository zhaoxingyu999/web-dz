using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Dualpsy.Tripartite.SMS
{
    public class AlidayuSMS
    {
        //产品名称:云通信短信API产品,开发者无需替换
        const String product = "Dysmsapi";
        //产品域名,开发者无需替换
        const String domain = "dysmsapi.aliyuncs.com";
        private const string App_Url = "http://gw.api.taobao.com/router/rest";
        //private const string App_Url = "http://gw.api.tbsandbox.com/router/rest";
        private const string App_Key = "LTAIDBbth1RoXhJP";
        private const string App_Secret = "i9h9UpBDFYtzuNVCWciaJGXR9Mf0rH";

        string resultMsg = null;
        public string GetMessageError()
        {
            return resultMsg;
        }
        public int sendSms(string phone, string Content)
        {
            IClientProfile profile = Aliyun.Acs.Core.Profile.DefaultProfile.GetProfile("cn-hangzhou", App_Key, App_Secret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            SendSmsResponse response = null;
            try
            {

                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = phone;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = "一半";
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = "SMS_159627035";
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                //request.TemplateParam = "{\"customer\":\"123\"}";
                request.TemplateParam = "{\"code\":\"" + Content + "\"}";
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = "yourOutId";
                //请求失败这里会抛ClientException异常
                response = acsClient.GetAcsResponse(request);

            }
            catch (Aliyun.Acs.Core.Exceptions.ServerException e)
            {
                resultMsg = e.ErrorMessage;
                Console.WriteLine(e.ErrorCode);
            }
            catch (ClientException e)
            {
                resultMsg = e.ErrorMessage;
                Console.WriteLine(e.ErrorCode);
            }
            if (response == null)
            {
                return -1;
            }
            resultMsg = response.Message;
            if (response.Code != "OK")
            {
                return -1;
            }
            return 0;

        }
        /// <summary>
        /// 短信单发
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        [Obsolete("已经更新，使用sendSms", true)]
        public int Send(string phone, string Content)
        {
#if !FB
            return 0;
#endif
            ITopClient client = new DefaultTopClient(App_Url, App_Key, App_Secret, "json");
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = "";
            req.SmsType = "normal";
            req.SmsFreeSignName = "番茄卷";
            req.SmsParam = "{\"code\":\"" + Content + "\"}";
            req.RecNum = phone;
            req.SmsTemplateCode = "SMS_143711741";
            //if (type == SMSType.CODE)
            //    req.SmsTemplateCode = "SMS_67180723";
            //else if (type == SMSType.SMS)
            //    req.SmsTemplateCode = "SMS_67125710";
            //else if (type == SMSType.TEST)
            //{
            //    req.SmsFreeSignName = "注册验证";
            //    req.SmsTemplateCode = "SMS_671355717";
            //    req.SmsParam = "{\"code\":\"" + Content + "\",\"product\":\"百业宝\"}";
            //}
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            resultMsg = rsp.Body;
            if (rsp.IsError)
                return int.Parse(rsp.ErrCode);
            return 0;
        }

    }
}
