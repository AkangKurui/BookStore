using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edument.CQRS;
using BookStoreReadModels;
using BookStore;

namespace BookStoreWeb
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IBookStoreQuery BookStoreQuery;
        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());
            BookStoreQuery = new BookStoreQuery();

            Dispatcher.ScanInstance(BookStoreQuery);
            Dispatcher.ScanInstance(new BookStoreAggregate());
        }
    }
}