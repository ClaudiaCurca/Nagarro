using iQuest.BooksAndNews.Application.Publications;
using iQuest.BooksAndNews.Application.Publishers;
using System;

namespace iQuest.BooksAndNews.Application.Subscribers
{
    public class BookLover
    {
        public string Name { get; set; }
        public ILog log;

        public BookLover(string name, PrintingOffice printingOffice, ILog log)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            printingOffice.bookListeners += HandleNewBookEvent;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public void HandleNewBookEvent(Book book)
        {
            log.WriteInfo($"Book Lover {Name} Received the book {book.Title}");

        }
    }
}