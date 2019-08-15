using System;
using FilmLove.API.Controllers;
using FilmLove.API.Models;
using FilmLove.Database.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmLove.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SmsController sc = new SmsController();
            AccountController ac = new AccountController();
            InputSendSms input = new InputSendSms()
            {
                phone = "13179960679",
                type = EnumSms.itype.RegLogin,
                bingtoken = "133"
            };
            sc.GetValidate(input);
            sc.CheckValidate("13179960679", EnumSms.itype.RegLogin, "123456", "1");
            ac.UpdatePwd("111111", "123456");
        }
    }
}
