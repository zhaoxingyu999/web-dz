using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FilmLove.API.Controllers
{
    public class JsonController : Controller
    {
        override protected JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {

            if ((behavior == System.Web.Mvc.JsonRequestBehavior.DenyGet) && string.Equals(this.HttpContext!=null?this.HttpContext.Request.HttpMethod:"post", "GET", StringComparison.OrdinalIgnoreCase))
            {
                return new JsonResult();
            }
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
            /*
            return new JsonResult { Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior };
             */
        }
    }
    /// <summary>
    /// http://www.cnblogs.com/JerryWang1991/archive/2013/03/08/2950457.html
    /// </summary>
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings { ContractResolver = new NullToEmptyStringResolver() };
                //var str = JsonConvert.SerializeObject(Data, Formatting.None,
                //    new JsonConverter[] { new DecimalConverter(), new DateTimeConverter() });
                var decConverter = new DecimalConverter();
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var str = JsonConvert.SerializeObject(Data, Formatting.Indented, new JsonConverter[] { decConverter, timeConverter });
                response.Write(str);
            }
        }
        public class DecimalConverter : CustomCreationConverter<decimal>
        {
            public override decimal Create(Type objectType)
            {
                return 0.00M;
            }
            public override bool CanWrite => true;

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((decimal)value).ToString("#0.00"));
            }
        }
        public class DateTimeConverter : CustomCreationConverter<DateTime>
        {
            public override DateTime Create(Type objectType)
            {
                return DateTime.Now;
            }
            public override bool CanWrite => true;

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        public class NullToEmptyStringResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return type.GetProperties()
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    return jp;
                }).ToList();

            }
        }
        public class NullToEmptyStringValueProvider : Newtonsoft.Json.Serialization.IValueProvider
        {
            PropertyInfo _MemberInfo;
            public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
            {
                _MemberInfo = memberInfo;
            }

            public object GetValue(object target)
            {
                object result = _MemberInfo.GetValue(target);
                if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
                else if (_MemberInfo.PropertyType == typeof(decimal?) && result == null) result = 0.00M;
                else if (_MemberInfo.PropertyType == typeof(int?) && result == null) result = 0;
                return result;
            }

            public void SetValue(object target, object value)
            {
                _MemberInfo.SetValue(target, value);
            }
        }
    }
}