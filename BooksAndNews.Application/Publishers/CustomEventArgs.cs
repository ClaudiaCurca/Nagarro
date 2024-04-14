using System;

namespace iQuest.BooksAndNews.Application.Publishers
{
    public class CustomEventArgs<T> : EventArgs 
    {
        public CustomEventArgs(T documet)
        {
            this.document = documet;
        }
        public T document { get; set; }
    }
}