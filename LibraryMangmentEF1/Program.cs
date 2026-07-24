using LibraryMangmentEF1;
using LibraryMangmentEF1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
      
        using (var context = new LibraryContext())
        {
            SeedData.Initialize(context);
        }

        // 3. حلقة القائمة التفاعلية للبرنامج
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. List All Books");
            Console.WriteLine("2. Search Book by Title");
            Console.WriteLine("3. Add New Book");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Return a Book");
            Console.WriteLine("6. Delete a Specific Book");
            Console.WriteLine("7. Delete All Books");
            Console.WriteLine("8. Exit");
            Console.Write("\nChoose an option: ");

            string choice = Console.ReadLine() ?? string.Empty;
            Console.Clear();

            switch (choice)
            {
                case "1":
                    ListAllBooks();
                    break;
                case "2":
                    SearchBooks();
                    break;
                case "3":
                    AddBook();
                    break;
                case "4":
                    BorrowBookProcess();
                    break;
                case "5":
                    ReturnBookProcess();
                    break;
                case "6":
                    DeleteBookProcess();
                    break;
                case "7":
                    DeleteAllBooksProcess();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void ListAllBooks()
    {
        using var context = new LibraryContext();
        var books = context.Books.Include(b => b.Author).ToList();

        Console.WriteLine("--- Books List ---");
        if (!books.Any())
        {
            Console.WriteLine("No books found in the database.");
            return;
        }

        foreach (var b in books)
        {
            string status = b.IsAvailable ? "Available" : "Borrowed";
            Console.WriteLine($"[ID: {b.Id}] {b.Title} | Author: {b.Author.Name} | Year: {b.PublishYear} | Status: {status}");
        }
    }

    static void SearchBooks()
    {
        Console.Write("Enter keyword to search: ");
        string keyword = Console.ReadLine() ?? string.Empty;

        using var context = new LibraryContext();
        var results = context.Books
            .Include(b => b.Author)
            .Where(b => b.Title.Contains(keyword))
            .ToList();

        Console.WriteLine($"--- Search Results for: ({keyword}) ---");
        if (!results.Any())
        {
            Console.WriteLine("No matching books found.");
            return;
        }

        foreach (var b in results)
        {
            Console.WriteLine($"[ID: {b.Id}] {b.Title} - Author: {b.Author.Name} (Available: {b.IsAvailable})");
        }
    }

    static void AddBook()
    {
        using var context = new LibraryContext();

        Console.Write("Enter book title: ");
        string title = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter publish year: ");
        if (!int.TryParse(Console.ReadLine(), out int year)) year = 2026;

        var authors = context.Authors.ToList();
        if (!authors.Any())
        {
            Console.WriteLine("No authors found in database! Please add an author first.");
            return;
        }

        Console.WriteLine("\nAvailable Authors:");
        foreach (var a in authors)
        {
            // 👈 هذا السطر هو المسؤول عن طباعة الرقم والاسم معاً بشكل صحيح
            Console.WriteLine($"{a.Id}. {a.Name}");
        }

        Console.Write("Enter Author ID: ");
        if (!int.TryParse(Console.ReadLine(), out int authorId) || !context.Authors.Any(a => a.Id == authorId))
        {
            Console.WriteLine("Invalid Author ID!");
            return;
        }

        var newBook = new Book
        {
            Title = title,
            PublishYear = year,
            IsAvailable = true,
            AuthorId = authorId
        };

        context.Books.Add(newBook);
        context.SaveChanges();
        Console.WriteLine("Book added successfully!");
    }

    static void BorrowBookProcess()
    {
        using var context = new LibraryContext();

        ListAllBooks();
        Console.Write("\nEnter Book ID to borrow: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId)) return;

        var book = context.Books.Find(bookId);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (!book.IsAvailable)
        {
            Console.WriteLine("Sorry, this book is already borrowed!");
            return;
        }

        var members = context.Members.ToList();
        if (!members.Any())
        {
            Console.WriteLine("No members found in database!");
            return;
        }

        Console.WriteLine("\nRegistered Members:");
        foreach (var m in members)
            Console.WriteLine($"ID: {m.Id}. Name: {m.FullName}");

        Console.Write("Enter Member ID: ");
        if (!int.TryParse(Console.ReadLine(), out int memberId) || !context.Members.Any(m => m.Id == memberId))
        {
            Console.WriteLine("Invalid Member ID!");
            return;
        }

        var record = new BorrowRecord
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.Now,
            IsReturned = false
        };

        book.IsAvailable = false;
        context.BorrowRecords.Add(record);
        context.SaveChanges();

        Console.WriteLine("Book borrowed successfully and status updated!");
    }

    static void ReturnBookProcess()
    {
        using var context = new LibraryContext();

        var activeBorrows = context.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.Member)
            .Where(r => !r.IsReturned)
            .ToList();

        if (!activeBorrows.Any())
        {
            Console.WriteLine("No active borrowed books right now.");
            return;
        }

        Console.WriteLine("--- Active Borrow Records ---");
        foreach (var r in activeBorrows)
        {
            Console.WriteLine($"[Record ID: {r.Id}] Book: {r.Book.Title} | Member: {r.Member.FullName} | Date: {r.BorrowDate.ToShortDateString()}");
        }

        Console.Write("\nEnter Borrow Record ID to return: ");
        if (!int.TryParse(Console.ReadLine(), out int recordId)) return;

        var borrowRecord = context.BorrowRecords.Include(r => r.Book).FirstOrDefault(r => r.Id == recordId);
        if (borrowRecord == null)
        {
            Console.WriteLine("Record not found.");
            return;
        }

        borrowRecord.IsReturned = true;
        borrowRecord.ReturnDate = DateTime.Now;
        borrowRecord.Book.IsAvailable = true;

        context.SaveChanges();
        Console.WriteLine("Book returned successfully and marked as available!");
    }

    static void DeleteBookProcess()
    {
        using var context = new LibraryContext();

        var books = context.Books.Include(b => b.Author).ToList();
        if (!books.Any())
        {
            Console.WriteLine("No books available to delete.");
            return;
        }

        Console.WriteLine("--- Books List ---");
        foreach (var b in books)
        {
            Console.WriteLine($"[ID: {b.Id}] {b.Title} | Author: {b.Author.Name}");
        }

        Console.Write("\nEnter Book ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var book = context.Books
            .Include(b => b.BorrowRecords)
            .FirstOrDefault(b => b.Id == bookId);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (book.BorrowRecords.Any())
        {
            context.BorrowRecords.RemoveRange(book.BorrowRecords);
        }

        context.Books.Remove(book);
        context.SaveChanges();

        Console.WriteLine("Book and its related records deleted successfully from database!");
    }

    static void DeleteAllBooksProcess()
    {
        using var context = new LibraryContext();

        if (!context.Books.Any())
        {
            Console.WriteLine("The library is already empty. No books to delete.");
            return;
        }

        Console.Write("Are you sure you want to delete ALL books? (y/n): ");
        string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (confirmation != "y")
        {
            Console.WriteLine("Deletion cancelled.");
            return;
        }

        if (context.BorrowRecords.Any())
        {
            context.BorrowRecords.RemoveRange(context.BorrowRecords);
        }

        context.Books.RemoveRange(context.Books);
        context.SaveChanges();

        Console.WriteLine("All books and their related records have been deleted successfully from the database!");
    }
}