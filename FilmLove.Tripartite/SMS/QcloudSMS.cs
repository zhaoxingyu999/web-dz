using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dualpsy.Tripartite.SMS
{
    public class QcloudSMS
    {
        // 短信应用SDK AppID
        int appid = 1400191799;

        // 短信应用SDK AppKey
        string appkey = "9c20944680d7c8f63249cac52ce0b8db";


        string resultMsg = null;
        public int BatchSend(List<string> phones, string Content)
        {
            throw new NotImplementedException();
        }

        public string GetMessageError()
        {
            return resultMsg;
        }

        public int Send(string phone, SMSType type)
        {
            throw new NotImplementedException();
        }

        public int Send(string phone, string Content, SMSType type)
        {
#if !FB
            return 0;
#endif
            SmsSingleSenderResult result = null;
            try
            {
                resultMsg = "";
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                result = ssender.sendWithParam("86", phone,
                    289735, new[] { Content }, "单身狗APP", "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
                Console.WriteLine(result);
                resultMsg = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (JSONException e)
            {
                resultMsg = e.Message;
                Console.WriteLine(e);
                return 1;
            }
            catch (HTTPException e)
            {
                resultMsg = e.Message;
                Console.WriteLine(e);
                return 1;
            }
            catch (Exception e)
            {
                resultMsg = e.Message;
                Console.WriteLine(e);
                return 1;
            }
            if (result.result != 0)
            {
                resultMsg = result.errMsg;
                return 1;
            }
            return 0;
        }
    }
}
