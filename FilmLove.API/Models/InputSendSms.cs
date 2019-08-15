using FilmLove.Database.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmLove.API.Models
{
    public class InputSendSms
    {
        [StringLength(11, ErrorMessage = "手机号码必须是11位", MinimumLength = 11)]
        [Required(ErrorMessage = "手机号码为空")]
        public string phone { get; set; }
        public EnumSms.itype type { get; set; }
        public string bingtoken { get; set; }
    }
}