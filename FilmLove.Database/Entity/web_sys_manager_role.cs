using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_manager_role
    /// </summary>
    public partial class WebSysManagerRole
    {
        /// <summary>
        /// auto_id
        /// </summary>
        [Display(Name = "auto_id")]
        public int AutoId { get; set; }
        /// <summary>
        /// manager_id
        /// </summary>
        [Display(Name = "manager_id")]
        public int? ManagerId { get; set; }
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int? RoleId { get; set; }
    }
}