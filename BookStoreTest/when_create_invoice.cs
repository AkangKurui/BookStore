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
    public class when_create_invoice : BDDTest<BookStoreAggregate>
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
        public void OpenInvoice()
        {
            Test(
                Given(),
                When(new CreateInvoice
                {
                    Id = transactionId,
                    Customer = custCmd,
                }),
                Then(new InvoiceCreated
                {
                    Id = transactionId,
                    Customer = custEv,
                    TransactionDate = DateTime.Now.Date,
                    DueDate = DateTime.Now.Date.AddDays(15),
                    InvoiceNo = "INV-"
                }));
        }
    }
}
