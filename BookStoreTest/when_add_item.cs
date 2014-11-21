using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Edument.CQRS;
using BookStore;
using BookStoreEvent;
using BookStore.command;

namespace BookStoreTest
{
    [TestFixture]
    public class when_add_item : BDDTest<BookStoreAggregate>
    {
        private static BookStore.entity.Customer custCmd;
        private static BookStoreEvent.Customer custEv;
        private static Guid custId;
        private static Guid transactionId;
        private static Guid itemId;

        [SetUp]
        public void SetUp()
        {
            custId = Guid.NewGuid();
            transactionId = Guid.NewGuid();
            itemId = Guid.NewGuid();
            var term = new BookStore.valueobject.Term("001", 15);
            custCmd = new BookStore.entity.Customer(custId, "Gusti", "Batam", term);
            custEv = new BookStoreEvent.Customer
            {
                custId = custId,
                Address = "Batam",
                Name = "Gusti",
                Term = new Term
                {
                    Code = "001",
                    Value = 15
                }
            };
        }

        [Test]
        public void AddItem()
        {
            Test(
                Given(new InvoiceCreated
                {
                    Id = transactionId,
                    Customer = custEv,
                }),
                When(new AddItem
                {
                    Id = transactionId,
                    ItemId = itemId,
                    Barcode = "GT-001",
                    Name = "Kilat Langkah Menuju Surga",
                    Price = 25000,
                    Qty = 1
                }),
                Then(new ItemAdded
                {
                    Id = transactionId,
                    ItemId = itemId,
                    Barcode = "GT-001",
                    Name = "Kilat Langkah Menuju Surga",
                    Price = 25000,
                    Qty = 1,
                    Total = 25000
                }, new SummaryChanged
                {
                    Id = transactionId,
                    SubTotal = 25000,
                    NetTotal = 25000
                }));
        }
    }
}
