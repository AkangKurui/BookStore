using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class InvoiceCreated
    {
        public Guid Id;
        public Customer Customer;
        public string InvoiceNo;
        public DateTime DueDate;
        public DateTime TransactionDate;
        public double SubTotal;
        public double NetTotal;
    }
}
