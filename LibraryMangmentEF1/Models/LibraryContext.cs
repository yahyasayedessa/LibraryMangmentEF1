using Microsoft.EntityFrameworkCore;
using LibraryMangmentEF1.Models;

namespace LibraryMangmentEF1
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ربط السيرفر وتأكيد الاعتمادية الأمنية
            optionsBuilder.UseSqlServer("Server=DESKTOP-NF75LJC\\SQLEXPRESS01;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ضبط العلاقات بدقة
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // إضافة بيانات أولية (Seed Data) للتأكد من جاهزية النظام فور التشغيل
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "yahya sayed" },
                new Author { Id = 2, Name = "mohamed essa" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Clean Code", PublishYear = 2008, IsAvailable = true, AuthorId = 1 },
                new Book { Id = 2, Title = "Modern Operating Systems", PublishYear = 2014, IsAvailable = true, AuthorId = 2 }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, FullName = "samer samra", Email = "ahmad@uni.edu" }
            );
        }
    }
}