using Blog40.Models;
using System;
using System.Web.Mvc;

namespace Blog40.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public Author Author { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}