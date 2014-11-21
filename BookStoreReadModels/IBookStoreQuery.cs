using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStoreReadModels.Models;

namespace BookStoreReadModels
{
    public interface IBookStoreQuery
    {
        List<InvoiceReport> GetAllInvoice();
        InvoiceReport GetInvoice(Guid Id);
        List<ItemReport> GetItems(Guid invoiceId);
        ItemReport GetItem(Guid id);
    }
}
