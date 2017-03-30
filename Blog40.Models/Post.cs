using System;

namespace Blog40.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public Author Author { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool isDeleted { get; set; }

        public Post()
        {
            this.Category = new Category();
            this.Author = new Author();
        }    
    }
}