using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_log
    /// </summary>
    public partial class WebSysLog
    {
        /// <summary>
        /// id
        /// </summary>
        [Display(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// manager_guid
        /// </summary>
        [Display(Name = "manager_guid")]
        public string ManagerGuid { get; set; }
        /// <summary>
        /// log_type
        /// </summary>
        [Display(Name = "log_type")]
        public int? LogType { get; set; }
        /// <summary>
        /// log_content
        /// </summary>
        [Display(Name = "log_content")]
        public string LogContent { get; set; }
        /// <summary>
        /// log_time
        /// </summary>
        [Display(Name = "log_time")]
        public DateTime? LogTime { get; set; }
        /// <summary>
        /// log_name
        /// </summary>
        [Display(Name = "log_name")]
        public string LogName { get; set; }
        /// <summary>
        /// manager_account
        /// </summary>
        [Display(Name = "manager_account")]
        public string ManagerAccount { get; set; }
        /// <summary>
        /// map_method
        /// </summary>
        [Display(Name = "map_method")]
        public string MapMethod { get; set; }
        /// <summary>
        /// log_ip
        /// </summary>
        [Display(Name = "log_ip")]
        public string LogIp { get; set; }
    }
}