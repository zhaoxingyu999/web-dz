using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilmLove.Database.Entity
{
    /// <summary>
    ///Product
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [Display(Name = "Type")]
        public int? Type { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        [Display(Name = "Content")]
        public string Content { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [Display(Name = "Title")]
        public string Title { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        [Display(Name = "UpdateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}