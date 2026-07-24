using System.Collections.Generic;

namespace LibraryMangmentEF1.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // العلاقة: المؤلف الواحد له عدة كتب
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}