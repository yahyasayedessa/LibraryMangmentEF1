using System.Collections.Generic;

namespace LibraryMangmentEF1.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        // سجلة استعارات العضو
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
