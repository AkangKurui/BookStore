using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edument.CQRS;
using BookStoreEvent;
using System.Collections;
using BookStore.command;
using BookStore.entity;

namespace BookStore
{
    public class BookStoreAggregate : Aggregate,
        IHandleCommand<CreateInvoice>,
        IHandleCommand<ChangeTransactionDate>,
        IHandleCommand<ChangeTerm>,
        IHandleCommand<AddItem>,
        IHandleCommand<ChangeQty>,
        IHandleCommand<ChangeCustomer>,
        IHandleCommand<DeleteInvoice>,
        IApplyEvent<InvoiceCreated>,
        IApplyEvent<ItemAdded>,
        IApplyEvent<QtyChanged>,
        IApplyEvent<SummaryChanged>,
        IApplyEvent<TransactionDateChanged>,
        IApplyEvent<CustomerChanged>
    {
        private BookStore.valueobject.Term term = null;
        private List<Item> items = new List<Item>();
        private DateTime transactionDate = DateTime.Now.Date;
        public IEnumerable Handle(CreateInvoice c)
        {
            transactionDate = DateTime.Now.Date;
            var dueDate = transactionDate.AddDays(c.Customer.Term.Value);
            var transNo = "INV-";
            yield return new InvoiceCreated
            {
                Id = c.Id,
                Customer = new BookStoreEvent.Customer
                {
                    Name = c.Customer.Name,
                    Address = c.Customer.Address,
                    custId = c.Customer.Id,
                    Term = new BookStoreEvent.Term
                    {
                        Code = c.Customer.Term.Code,
                        Value = c.Customer.Term.Value
                    }
                },
                DueDate = dueDate,
                InvoiceNo = transNo,
                TransactionDate = transactionDate,
                SubTotal = 0,
                NetTotal = 0
            };
        }

        public IEnumerable Handle(ChangeTransactionDate c)
        {
            transactionDate = c.TransactionDate.Date;
            var dueDate = c.TransactionDate.Date.AddDays(term.Value);
            yield return new TransactionDateChanged
            {
                Id = c.Id,
                TransactionDate = transactionDate,
                DueDate = dueDate
            };
        }

        public IEnumerable Handle(ChangeTerm c)
        {
            yield return new TermChanged
            {
                Id = c.Id,
                Code = c.Code,
                Value = c.Value
            };
        }

        public IEnumerable Handle(AddItem c)
        {
            var item = new Item();
            items.Add(item);
            yield return item.Handle(c);
            SummaryChanged summaryChanged = new SummaryChanged
            {
                Id = c.Id,
                SubTotal = items.Sum(i => i.Total),
                NetTotal = items.Sum(i => i.Total)
            };
            yield return summaryChanged;
        }

        public IEnumerable Handle(ChangeQty c)
        {
            var item = items.FirstOrDefault(x => x.ItemId == c.ItemId);
            yield return item.Handle(c);
            SummaryChanged summaryChanged = new SummaryChanged
            {
                Id = c.Id,
                SubTotal = items.Sum(i => i.Total),
                NetTotal = items.Sum(i => i.Total)
            };
            yield return summaryChanged;
        }

        public IEnumerable Handle(DeleteInvoice c)
        {
            yield return new InvoiceDeleted
            {
                Id = c.Id
            };
        }

        public IEnumerable Handle(ChangeCustomer c)
        {
            term = new valueobject.Term(c.Customer.Term.Code, c.Customer.Term.Value);
            yield return new CustomerChanged
            {
                Id = c.Id,
                Customer = new BookStoreEvent.Customer
                {
                    Address = c.Customer.Address,
                    custId = c.Customer.Id,
                    Name = c.Customer.Name,
                    Term = new BookStoreEvent.Term
                    {
                        Code = c.Customer.Term.Code,
                        Value = c.Customer.Term.Value
                    }
                },
                DueDate = transactionDate.AddDays(term.Value)
            };
        }

        public void Apply(InvoiceCreated e)
        {
            transactionDate = e.TransactionDate.Date;
            term = new valueobject.Term(e.Customer.Term.Code, e.Customer.Term.Value);
        }

        public void Apply(ItemAdded e)
        {
            var item = new Item();
            item.Apply(e);
            items.Add(item);
        }

        public void Apply(QtyChanged e)
        {
            var item = items.FirstOrDefault(x => x.ItemId == e.ItemId);
            item.Apply(e);
        }

        public void Apply(SummaryChanged e)
        {
        }

        public void Apply(TransactionDateChanged e)
        {
            transactionDate = e.TransactionDate.Date;
        }

        public void Apply(CustomerChanged e)
        {
            term = new valueobject.Term(e.Customer.Term.Code, e.Customer.Term.Value);
        }
    }
}
