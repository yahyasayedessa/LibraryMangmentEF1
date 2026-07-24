using LibraryMangmentEF1.Models;
using System.Linq;

namespace LibraryMangmentEF1
{
    public static class SeedData
    {
        public static void Initialize(LibraryContext context)
        {
            // إذا كان جدول المؤلفين فارغاً، أضف بيانات افتراضية
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author { Name = "yahya sayed" },
                    new Author { Name = "mohamed essa" }
                );
                context.SaveChanges();
            }

            // إذا كان جدول الكتب فارغاً، أضف كتباً مرتبطة بالمؤلف الأول
            if (!context.Books.Any())
            {
                var author = context.Authors.FirstOrDefault();
                if (author != null)
                {
                    context.Books.AddRange(
                        new Book { Title = "Clean Code", PublishYear = 2008, IsAvailable = true, AuthorId = author.Id },
                        new Book { Title = "C# Programming", PublishYear = 2022, IsAvailable = true, AuthorId = author.Id }
                    );
                    context.SaveChanges();
                }
            }

            // إذا كان جدول الأعضاء فارغاً، أضف عضواً افتراضياً
            if (!context.Members.Any())
            {
                context.Members.Add(
                    new Member { FullName = "samer samra", Email = "samer@uni.com" }
                );
                context.SaveChanges();
            }
        }
    }
}