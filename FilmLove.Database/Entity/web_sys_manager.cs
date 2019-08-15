using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_manager
    /// </summary>
    public partial class WebSysManager
    {
        /// <summary>
        /// manager_id
        /// </summary>
        [Display(Name = "manager_id")]
        public int ManagerId { get; set; }
        /// <summary>
        /// manager_name
        /// </summary>
        [Display(Name = "manager_name")]
        public string ManagerName { get; set; }
        /// <summary>
        /// manager_pwd
        /// </summary>
        [Display(Name = "manager_pwd")]
        public string ManagerPwd { get; set; }
        /// <summary>
        /// manager_scal
        /// </summary>
        [Display(Name = "manager_scal")]
        public string ManagerScal { get; set; }
        /// <summary>
        /// manager_realname
        /// </summary>
        [Display(Name = "manager_realname")]
        public string ManagerRealname { get; set; }
        /// <summary>
        /// manager_tel
        /// </summary>
        [Display(Name = "manager_tel")]
        public string ManagerTel { get; set; }
        /// <summary>
        /// manager_email
        /// </summary>
        [Display(Name = "manager_email")]
        public string ManagerEmail { get; set; }
        /// <summary>
        /// manager_isdel
        /// </summary>
        [Display(Name = "manager_isdel")]
        public int? ManagerIsdel { get; set; }
        /// <summary>
        /// manager_status
        /// </summary>
        [Display(Name = "manager_status")]
        public int? ManagerStatus { get; set; }
        /// <summary>
        /// create_time
        /// </summary>
        [Display(Name = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// update_time
        /// </summary>
        [Display(Name = "update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// is_supper
        /// </summary>
        [Display(Name = "is_supper")]
        public int? IsSupper { get; set; }
        /// <summary>
        /// last_login_time
        /// </summary>
        [Display(Name = "last_login_time")]
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// cur_token
        /// </summary>
        [Display(Name = "cur_token")]
        public string CurToken { get; set; }
    }
}