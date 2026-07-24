using System;

namespace LibraryMangmentEF1.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; } // فارغة إذا لم يتم الإرجاع بعد
        public bool IsReturned { get; set; } = false;
    }
}