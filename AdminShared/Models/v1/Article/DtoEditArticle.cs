﻿using System.ComponentModel.DataAnnotations;

namespace AdminShared.Models.v1.Article
{

    /// <summary>
    /// 创建文章
    /// </summary>
    public class DtoEditArticle
    {


        public DtoEditArticle(string title, string content)
        {
            Title = title;
            Content = content;
        }


        /// <summary>
        /// 类别ID
        /// </summary>
        public long CategoryId { get; set; }



        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不可以空")]
        public string Title { get; set; }



        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不可以空")]
        public string Content { get; set; }



        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }



        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }



        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }



        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickCount { get; set; }



        /// <summary>
        /// 摘要
        /// </summary>
        public string? Digest { get; set; }


    }
}
