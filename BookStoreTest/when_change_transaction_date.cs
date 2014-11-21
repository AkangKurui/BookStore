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
    public class when_change_transaction_date : BDDTest<BookStoreAggregate>
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
        public void ChangeTransactionDate()
        {
            Test(
                Given(new InvoiceCreated
                {
                    Id = transactionId,
                    Customer = custEv,
                }),
                When(new ChangeTransactionDate
                {
                    Id = transactionId,
                    TransactionDate = new DateTime(2014, 10, 26),
                }),
                Then(new TransactionDateChanged
                {
                    Id = transactionId,
                    TransactionDate = new DateTime(2014, 10, 26),
                    DueDate = new DateTime(2014, 11, 10)
                }));
        }
    }
}
