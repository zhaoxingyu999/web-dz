using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///Copyright
    /// </summary>
    public partial class Copyright
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        public int Id { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string Tel { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { get; set; }
        /// <summary>
        /// 记录更新时间
        /// </summary>
        [Display(Name = "记录更新时间")]
        public DateTime? UpdateTime { get; set; }
    }
}