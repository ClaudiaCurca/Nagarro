using iQuest.BooksAndNews.Application.Publications;
using iQuest.BooksAndNews.Application.Publishers;
using System;

namespace iQuest.BooksAndNews.Application.Subscribers
{
    public class NewsHunter
    {
        public string Name{ set; get; }
        public ILog log;

        public NewsHunter(string name, PrintingOffice printingOffice, ILog log)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            printingOffice.newsListeners += HandleCustomEvent;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        private void HandleCustomEvent(Newspaper newspaper)
        {
            log.WriteInfo($"News Hunter {Name} Received the newspaper {newspaper.Title}");
        }
    }
}