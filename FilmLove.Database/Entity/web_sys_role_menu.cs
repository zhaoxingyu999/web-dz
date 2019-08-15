using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_role_menu
    /// </summary>
    public partial class WebSysRoleMenu
    {
        /// <summary>
        /// auto_id
        /// </summary>
        [Display(Name = "auto_id")]
        public int AutoId { get; set; }
        /// <summary>
        /// role_id
        /// </summary>
        [Display(Name = "role_id")]
        public int? RoleId { get; set; }
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int? MenuId { get; set; }
        /// <summary>
        /// page_ids
        /// </summary>
        [Display(Name = "page_ids")]
        public string PageIds { get; set; }
    }
}