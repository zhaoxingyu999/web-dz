using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///web_sys_menu_page
    /// </summary>
    public partial class WebSysMenuPage
    {
        /// <summary>
        /// page_id
        /// </summary>
        [Display(Name = "page_id")]
        public int PageId { get; set; }
        /// <summary>
        /// menu_id
        /// </summary>
        [Display(Name = "menu_id")]
        public int? MenuId { get; set; }
        /// <summary>
        /// main_status
        /// </summary>
        [Display(Name = "main_status")]
        public int? MainStatus { get; set; }
        /// <summary>
        /// page_name
        /// </summary>
        [Display(Name = "page_name")]
        public string PageName { get; set; }
        /// <summary>
        /// page_status
        /// </summary>
        [Display(Name = "page_status")]
        public int? PageStatus { get; set; }
        /// <summary>
        /// page_viewname
        /// </summary>
        [Display(Name = "page_viewname")]
        public string PageViewname { get; set; }
        /// <summary>
        /// page_btnname
        /// </summary>
        [Display(Name = "page_btnname")]
        public string PageBtnname { get; set; }
        /// <summary>
        /// page_type
        /// </summary>
        [Display(Name = "page_type")]
        public int? PageType { get; set; }
        /// <summary>
        /// page_url
        /// </summary>
        [Display(Name = "page_url")]
        public string PageUrl { get; set; }
        /// <summary>
        /// page_paramters
        /// </summary>
        [Display(Name = "page_paramters")]
        public string PageParamters { get; set; }
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
    }
}