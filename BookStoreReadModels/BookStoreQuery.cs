using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edument.CQRS;
using BookStoreEvent;
using BookStoreReadModels.Models;

namespace BookStoreReadModels
{
    public class BookStoreQuery : IBookStoreQuery,
        ISubscribeTo<InvoiceCreated>,
        ISubscribeTo<TransactionDateChanged>,
        ISubscribeTo<TermChanged>,
        ISubscribeTo<ItemAdded>,
        ISubscribeTo<CustomerChanged>,
        ISubscribeTo<QtyChanged>,
        ISubscribeTo<SummaryChanged>,
        ISubscribeTo<InvoiceDeleted>
    {
        public List<InvoiceReport> invoice = new List<InvoiceReport>();
        public List<ItemReport> items = new List<ItemReport>();
        public int transNo = 1;

        public List<InvoiceReport> GetAllInvoice()
        {
            return invoice;
        }

        public InvoiceReport GetInvoice(Guid id)
        {
            return invoice.FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<ItemReport> GetItems(Guid invoiceId)
        {
            return items.Where(x => x.Id == invoiceId).ToList();
        }

        public ItemReport GetItem(Guid id)
        {
            return items.FirstOrDefault(x => x.ItemId.Equals(id));
        }

        public void Handle(InvoiceCreated e)
        {
            var report = new InvoiceReport
            {
                Id = e.Id,
                Customer = new CustomerReport
                {
                    Id = e.Customer.custId,
                    Address = e.Customer.Address,
                    Name = e.Customer.Name,
                    Term = new TermReport
                    {
                        Code = e.Customer.Term.Code,
                        Value = e.Customer.Term.Value
                    }
                },
                TransactionDate = e.TransactionDate,
                DueDate = e.DueDate,
                InvoiceNo = e.InvoiceNo + transNo++,
                SubTotal = e.SubTotal,
                NetTotal = e.NetTotal
            };
            lock (invoice)
            {
                invoice.Add(report);
            }
        }

        public void Handle(TransactionDateChanged e)
        {
            var invoice = GetInvoice(e.Id);
            invoice.TransactionDate = e.TransactionDate;
            invoice.DueDate = e.DueDate;
        }

        public void Handle(TermChanged e)
        {
            var invoice = GetInvoice(e.Id);
            invoice.Customer.Term.Code = e.Code;
            invoice.Customer.Term.Value = e.Value;
        }

        public void Handle(ItemAdded e)
        {
            var item = new ItemReport
            {
                Id = e.Id,
                Barcode = e.Barcode,
                ItemId = e.ItemId,
                Name = e.Name,
                Price = e.Price,
                Qty = e.Qty,
                Total = e.Total
            };

            lock (item)
            {
                items.Add(item);
            }
        }

        public void Handle(CustomerChanged e)
        {
            var invoice = GetInvoice(e.Id);
            invoice.Customer = new CustomerReport
            {
                Address = e.Customer.Address,
                Id = e.Customer.custId,
                Name = e.Customer.Name,
                Term = new TermReport
                {
                    Code = e.Customer.Term.Code,
                    Value = e.Customer.Term.Value
                }
            };
        }

        public void Handle(QtyChanged e)
        {
            var item = GetItem(e.ItemId);
            item.Qty = e.Qty;
            item.Total = e.Total;
        }

        public void Handle(SummaryChanged e)
        {
            var invoice = GetInvoice(e.Id);
            invoice.SubTotal = e.SubTotal;
            invoice.NetTotal = e.NetTotal;
        }

        public void Handle(InvoiceDeleted e)
        {
            invoice = invoice.Except(invoice.Where(x => x.Id.Equals(e.Id))).ToList();
        }
    }
}
