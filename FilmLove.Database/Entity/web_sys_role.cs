using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_role
    /// </summary>
    public partial class WebSysRole
    {
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int RoleId { get; set; }
        /// <summary>
        /// role_name
        /// </summary>
        [Display(Name = "role_name")]
        public string RoleName { get; set; }
        /// <summary>
        /// role_status
        /// </summary>
        [Display(Name = "role_status")]
        public int? RoleStatus { get; set; }
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
        /// role_remark
        /// </summary>
        [Display(Name = "role_remark")]
        public string RoleRemark { get; set; }
    }
}