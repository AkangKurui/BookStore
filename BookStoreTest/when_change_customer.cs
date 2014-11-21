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
    public class when_change_customer : BDDTest<BookStoreAggregate>
    {
        private static BookStore.entity.Customer custCmd;
        private static BookStoreEvent.Customer custEv;
        private static Guid custId;
        private static Guid custIdChange;
        private static Guid transactionId;
        private static Guid itemId;
        private static DateTime transactionDate;

        [SetUp]
        public void SetUp()
        {
            custId = Guid.NewGuid();
            transactionId = Guid.NewGuid();
            itemId = Guid.NewGuid();
            custIdChange = Guid.NewGuid();
            transactionDate = DateTime.Now.Date;
            var term = new BookStore.valueobject.Term("001", 15);
            custCmd = new BookStore.entity.Customer(custId, "Batam", "Gusti", term);
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
        public void ChangeCustomer()
        {
            Test(
                Given(new InvoiceCreated
                {
                    Id = transactionId,
                    Customer = custEv,
                    TransactionDate = transactionDate
                }),
                When(new ChangeCustomer
                {
                    Id = transactionId,
                    Customer = new BookStore.entity.Customer(custIdChange, "Chyan", "Tiban", new BookStore.valueobject.Term("002", 30))
                }),
                Then(new CustomerChanged
                {
                    Id = transactionId,
                    Customer = new Customer
                    {
                        Address = "Tiban",
                        custId = custIdChange,
                        Name = "Chyan",
                        Term = new BookStoreEvent.Term { Code = "002", Value = 30 }
                    },
                    DueDate = transactionDate.AddDays(30)
                }));
        }
    }
}
