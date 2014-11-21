using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore;
using BookStoreReadModels;
using Edument.CQRS;

namespace BookStoreAPI
{
    public class Domain
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