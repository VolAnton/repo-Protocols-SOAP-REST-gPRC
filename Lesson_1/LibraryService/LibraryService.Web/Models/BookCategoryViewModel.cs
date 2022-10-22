using LibraryServiceReference;

namespace LibraryService.Web.Models
{
    public class BookCategoryViewModel
    {
        public Book[] Books { get; set; }

        public SearchType SearchType { get; set; }

        public string? SearchString { get; set; }

    }
}
