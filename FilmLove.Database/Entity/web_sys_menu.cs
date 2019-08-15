using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_menu
    /// </summary>
    public partial class WebSysMenu
    {
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int MenuId { get; set; }
        /// <summary>
        /// menu_name
        /// </summary>
        [Display(Name = "menu_name")]
        public string MenuName { get; set; }
        /// <summary>
        /// menu_pid
        /// </summary>
        [Display(Name = "menu_pid")]
        public int? MenuPid { get; set; }
        /// <summary>
        /// menu_icon
        /// </summary>
        [Display(Name = "menu_icon")]
        public string MenuIcon { get; set; }
        /// <summary>
        /// index_code
        /// </summary>
        [Display(Name = "index_code")]
        public string IndexCode { get; set; }
        /// <summary>
        /// menu_url
        /// </summary>
        [Display(Name = "menu_url")]
        public string MenuUrl { get; set; }
        /// <summary>
        /// menu_status
        /// </summary>
        [Display(Name = "menu_status")]
        public int? MenuStatus { get; set; }
        /// <summary>
        /// menu_itempages
        /// </summary>
        [Display(Name = "menu_itempages")]
        public string MenuItempages { get; set; }
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
        /// menu_sort
        /// </summary>
        [Display(Name = "menu_sort")]
        public int? MenuSort { get; set; }
    }
}