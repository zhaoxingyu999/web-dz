using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///JobOffers
    /// </summary>
    public partial class JobOffers
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Display(Name = "岗位名称")]
        public string Name { get; set; }
        /// <summary>
        /// 岗位数量
        /// </summary>
        [Display(Name = "岗位数量")]
        public string Count { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        [Display(Name = "工作地点")]
        public string Position { get; set; }
        /// <summary>
        /// 岗位职责
        /// </summary>
        [Display(Name = "岗位职责")]
        public string Task { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [Display(Name = "Time")]
        public string Time { get; set; }
        /// <summary>
        /// 记录生成时间
        /// </summary>
        [Display(Name = "记录生成时间")]
        public int? IsActive { get; set; }
    }
}