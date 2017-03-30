namespace Blog40.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public bool IsDeleted { get; set; }
    }
}