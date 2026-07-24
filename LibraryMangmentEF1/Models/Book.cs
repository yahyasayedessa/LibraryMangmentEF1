namespace LibraryMangmentEF1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PublishYear { get; set; }
        public bool IsAvailable { get; set; } = true; // الحالة الافتتاحية: متاح للاستعارة

        // المفتاح الأجنبي والملاحة
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}