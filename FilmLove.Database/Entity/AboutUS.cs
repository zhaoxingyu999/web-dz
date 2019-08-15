using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///AboutUs
    /// </summary>
    public partial class AboutUs
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }
        /// <summary>
        /// 1.关于度载 2.企业使命
        /// </summary>
        [Display(Name = "1.关于度载 2.企业使命")]
        public int? Type { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        [Display(Name = "UpdateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}