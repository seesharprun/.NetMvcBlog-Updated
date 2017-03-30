using Blog40.Models;
using System.Collections.Generic;

namespace Blog40.ViewModels
{
    public class PostEditViewModel : PostViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}