using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///CarouselPhoto
    /// </summary>
    public partial class CarouselPhoto
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        [Display(Name = "唯一ID")]
        public int Id { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片地址")]
        public string ImgUrl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        [Display(Name = "详细内容")]
        public string Content { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Display(Name = "日期")]
        public string Time { get; set; }
        /// <summary>
        /// 轮播图描述
        /// </summary>
        [Display(Name = "轮播图描述")]
        public string Remark { get; set; }
        /// <summary>
        /// 记录生成时间
        /// </summary>
        [Display(Name = "记录生成时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 英文备注
        /// </summary>
        [Display(Name = "英文备注")]
        public string Description { get; set; }
        /// <summary>
        /// 作者(度载新闻)
        /// </summary>
        [Display(Name = "作者(度载新闻)")]
        public string Author { get; set; }
        /// <summary>
        /// 删除标识 1未删除 0删除
        /// </summary>
        [Display(Name = "删除标识 1未删除 0删除")]
        public int? IsActive { get; set; }
        /// <summary>
        /// 详细内容:大标题
        /// </summary>
        [Display(Name = "详细内容")]
        public string Headline { get; set; }
    }
}