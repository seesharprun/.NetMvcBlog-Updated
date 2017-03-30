namespace Blog40.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Slug { get; set; }

        public bool IsDeleted { get; set; }
    }
}