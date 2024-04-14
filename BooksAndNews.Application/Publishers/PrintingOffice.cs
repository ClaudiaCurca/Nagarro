using iQuest.BooksAndNews.Application.DataAccess;
using iQuest.BooksAndNews.Application.Publications;
using System;

namespace iQuest.BooksAndNews.Application.Publishers
{
    public delegate void Delegatebook(Book book);
    public delegate void Delegatenews(Newspaper news);

    public class PrintingOffice : IPrintingOffice
    {
        public Delegatebook bookListeners;
        public Delegatenews newsListeners;
        private IBookRepository bookRepository;
        private INewspaperRepository newspaperRepository;
        private ILog log;

        public PrintingOffice(IBookRepository bookRepository, INewspaperRepository newspaperRepository, ILog log)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.newspaperRepository = newspaperRepository ?? throw new ArgumentNullException(nameof(newspaperRepository));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void PrintRandom(int bookCount, int newspaperCount)
        {
            CreateBooks(bookCount);
            CreateNewspapers(newspaperCount);
        }

        private void CreateBooks(int bookCount)
        {
            Book book;
            for (int i = 0; i < bookCount; i++)
            {
                book = GenerateRandomBook();
                NotifyListenersNewBook(book);
            }
        }

        private Book GenerateRandomBook()
        {
            Book book = bookRepository.GetRandom();
            log.WriteInfo($"Generated book {book.Title} by {book.Author} {book.Year}");
            return book;
        }

        private void NotifyListenersNewBook(Book book)
        {
            bookListeners?.Invoke(book);
        }

        private Newspaper GenerateRandomNewspaper()
        {
            Newspaper newspaper = newspaperRepository.GetRandom();
            log.WriteInfo($"Generated newspaper {newspaper.Title} number {newspaper.Number}");
            return newspaper;
        }

        private void NotifyListenersNewNewspaper(Newspaper newspaper)
        {
            newsListeners?.Invoke(newspaper);
        }

        private void CreateNewspapers(int newspaperCount)
        {
            Newspaper newspaper;
            for (int i = 0; i < newspaperCount; i++)
            {
                newspaper = GenerateRandomNewspaper();
                NotifyListenersNewNewspaper(newspaper);
            }
        }

    }
}