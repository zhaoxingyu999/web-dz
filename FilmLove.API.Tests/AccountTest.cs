using System;
using FilmLove.API.Controllers;
using FilmLove.API.Models;
using FilmLove.API.Tests.utils;
using FilmLove.Database.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FilmLove.API.Tests
{
    [TestClass]
    public class AccountTest
    {
        AccountClear accountClear = new AccountClear();
        SmsClear smsClear = new SmsClear();

        public int Register(string phone)
        {
            AccountController account = new AccountController();
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = phone,
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0, "发送短信失败{0}", v1.msg);
            var v2 = sms.CheckValidate(input.phone, input.type, "123456", "").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 0, "验证短信失败{0}", v1.msg);
            var v = Newtonsoft.Json.JsonConvert.SerializeObject(v2.data);
            JObject data = Newtonsoft.Json.JsonConvert.DeserializeObject(v) as JObject;
            Assert.IsTrue(data["isExit"].Value<string>() == "1" && data["uid"] == null);
            var v3 = account.SetPwd(data["temptoken"].Value<string>(), input.phone, YJYSoft.YL.Common.Encrypt.MD5Encrypt("123456"), "0", "").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v3.code == 0, "注册用户失败{0}", v3.msg);

            return v3.data.uid;
        }

        [TestMethod]
        public void Reg()
        {
            string phone = "13101200140";
            Register(phone);
            accountClear.ClearUser(phone);
        }

        [TestMethod]
        public void Login()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200141",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            accountClear.AddUser(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            var v2 = sms.CheckValidate(input.phone, input.type, "123456", "").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 0);
            var v = Newtonsoft.Json.JsonConvert.SerializeObject(v2.data);
            Newtonsoft.Json.JsonConvert.SerializeObject(v2.data);
            JObject data = Newtonsoft.Json.JsonConvert.DeserializeObject(v) as JObject;
            Assert.IsTrue(data["uid"] != null);

            accountClear.ClearUser(input.phone);
        }
        [TestMethod]
        public void RegLoginValidErro()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200142",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0, "发送短信失败{0}", v1.msg);
            var v2 = sms.CheckValidate(input.phone, input.type, "123123", "").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 1010201, "验证码出错验证失败");
        }
        [TestMethod]
        public void LoginUserDisable()
        {
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200143",
                type = EnumSms.itype.RegLogin
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            accountClear.AddUser2(input.phone);
            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);
            var v2 = sms.CheckValidate(input.phone, input.type, "123456", "").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 1010205, "用户禁用验证失败");

            accountClear.ClearUser(input.phone);
        }

        [TestMethod]
        public void BingPhone()
        {
            AccountController account = new AccountController();
            ThreeLoginInfo three = new ThreeLoginInfo()
            {
                openid = "13101200144",
                //类型 1 wx,2 qq,3 wb
                type = 1,
                nickname = "unittest",
            };
            string token = JWTManager.Encode(three);
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200144",
                type = EnumSms.itype.BingPhone,
                bingtoken = token,
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);

            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);

            var v2 = sms.CheckValidate(input.phone, input.type, "123456", token).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 0, "绑定用户验证短信失败{0}", v2.msg);

            var v = Newtonsoft.Json.JsonConvert.SerializeObject(v2.data);
            Newtonsoft.Json.JsonConvert.SerializeObject(v2.data);
            JObject data = Newtonsoft.Json.JsonConvert.DeserializeObject(v) as JObject;
            var v3 = account.SetPwd(data["temptoken"].Value<string>(), input.phone, YJYSoft.YL.Common.Encrypt.MD5Encrypt("123456"), "1", token).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v3.code == 0, "绑定用户失败{0}", v3.msg);
            accountClear.ClearUser(input.phone);
        }
        [TestMethod]
        public void BingPhoneExistPhone()
        {
            AccountController account = new AccountController();
            ThreeLoginInfo three = new ThreeLoginInfo()
            {
                openid = "13101200145",
                //类型 1 wx,2 qq,3 wb
                type = 1,
                nickname = "unittest",
            };
            string token = JWTManager.Encode(three);
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200145",
                type = EnumSms.itype.BingPhone,
                bingtoken = token,
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            accountClear.AddUserBing(input.phone, three);

            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 1010102, "绑定用户验证短信失败{0}", v1.msg);

            //var v2 = sms.CheckValidate(input.phone, input.type, "123456", token).Data as YJYSoft.YL.Common.AjaxResult;
            //Assert.IsTrue(v2.code == 1010202, "手机号码已经绑定验证失败");
            accountClear.ClearUser(input.phone);
        }
        [TestMethod]
        public void BingPhoneExistBing()
        {
            AccountController account = new AccountController();
            ThreeLoginInfo three = new ThreeLoginInfo()
            {
                openid = "13101200146",
                //类型 1 wx,2 qq,3 wb
                type = 1,
                nickname = "unittest",
            };
            string token = JWTManager.Encode(three);
            SmsController sms = new SmsController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200146",
                type = EnumSms.itype.BingPhone,
                bingtoken = token,
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            accountClear.AddBing(input.phone, three);

            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);

            var v2 = sms.CheckValidate(input.phone, input.type, "123456", token).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 1010203, "已经绑定手机验证失败");
            accountClear.ClearUser(input.phone);
        }
        [TestMethod]
        public void FindPwd()
        {
            SmsController sms = new SmsController();
            AccountController account = new AccountController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13101200147",
                type = EnumSms.itype.RetrievePassword
            };
            smsClear.ClearSms(input.phone);
            accountClear.ClearUser(input.phone);
            accountClear.AddUser(input.phone);

            var v1 = sms.GetValidate(input).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0, "发送短信{0}", v1.msg);

            var v2 = account.FindPwd(input.phone, YJYSoft.YL.Common.Encrypt.MD5Encrypt("123456"), "123456").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 0, "找回密码{0}", v1.msg);
        }
        [TestMethod]
        public void UpdatePwd()
        {
            string phone = "13101200148";
            UpdatePwd(phone,"111111");
            accountClear.ClearUser(phone);
        }

        public void UpdatePwd(string phone,string pwd)
        {
            int uid = Register(phone);
            SmsController sms = new SmsController();
            AccountController account = new AccountController();
            account.userInfo = new APIUserInfo()
            {
                uid = uid,
            };
            sms.userInfo = new APIUserInfo()
            {
                uid = uid,
            };
            var v1 = sms.UpdateGetValidate(EnumSms.itype.ChangeLoginPasssword).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);

            var v2 = account.UpdatePwd(YJYSoft.YL.Common.Encrypt.MD5Encrypt(pwd), "123456").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 0);
        }
        [TestMethod]
        public void UpdatePwdError()
        {
            string phone = "13101200149";
            int uid = Register(phone);
            SmsController sms = new SmsController();
            AccountController account = new AccountController();
            account.userInfo = new APIUserInfo()
            {
                uid = uid,
            };
            sms.userInfo = new APIUserInfo()
            {
                uid = uid,
            };
            var v1 = sms.UpdateGetValidate(EnumSms.itype.ChangeLoginPasssword).Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v1.code == 0);

            var v2 = account.UpdatePwd(YJYSoft.YL.Common.Encrypt.MD5Encrypt("654321"), "123123").Data as YJYSoft.YL.Common.AjaxResult;
            Assert.IsTrue(v2.code == 1010201);
            accountClear.ClearUser(phone);
        }

        [TestMethod]
        public void PasswordLogin()
        {
            AccountController account = new AccountController();
            string phone = "13101200150";
            UpdatePwd(phone, "111111");
            account.PwdLogin(phone, YJYSoft.YL.Common.Encrypt.MD5Encrypt("111111"));
            accountClear.ClearUser(phone);
        }
    }
}
