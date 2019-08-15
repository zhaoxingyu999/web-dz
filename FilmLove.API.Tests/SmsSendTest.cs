using System;
using FilmLove.API.Controllers;
using FilmLove.API.Models;
using FilmLove.API.Tests.utils;
using FilmLove.Database.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmLove.API.Tests
{
    [TestClass]
    public class SmsSendTest
    {
        SmsClear smsClear = new SmsClear();

        [TestMethod]
        public void SendRetrievePassword()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200136",
                type = EnumSms.itype.RetrievePassword
            };
            smsClear.ClearSms(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            smsClear.ClearSms(input.phone);
        }
        [TestMethod]
        public void SendBingPhone()
        {
            ThreeLoginInfo three = new ThreeLoginInfo()
            {
                openid = "1",
                //类型 1 wx,2 qq,3 wb
                type = 1
            };
            string token = JWTManager.Encode(three);
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200135",
                type = EnumSms.itype.BingPhone,
                bingtoken = token
            };
            //清除绑定
            smsClear.ClearBing(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);

            smsClear.ClearBing(input.phone);
        }

        [TestMethod]
        public void SendBingPhoneExist()
        {
            ThreeLoginInfo three = new ThreeLoginInfo()
            {
                openid = "1",
                //类型 1 wx,2 qq,3 wb
                type = 1
            };
            string token = JWTManager.Encode(three);
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200134",
                type = EnumSms.itype.BingPhone,
                bingtoken = token
            };
            //添加用户
            smsClear.ClearBingTestUser(input.phone);
            smsClear.AddBingTestUser(input.phone, three.type);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 1010102);
            //清除用户
            smsClear.ClearBingTestUser(input.phone);
        }

        [TestMethod]
        public void SendRegLogin()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200133",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            smsClear.ClearSms(input.phone);

        }
        [TestMethod]
        public void TestTypeError()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200132",
                type = 0
            };
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 1010101);
        }
        [TestMethod]
        public void Send60Error()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200131",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            var v2 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
#if FB
            Assert.IsTrue(v2.code == 1010103);
#else
            Assert.IsTrue(v2.code == 0);
#endif
            smsClear.ClearSms(input.phone);
        }
        [TestMethod]
        public void Send60OK()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200130",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            smsClear.ChangeSendTime(input.phone);
            var v2 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;

            Assert.IsTrue(v2.code == 0);
            smsClear.ClearSms(input.phone);

        }
    }
}
