using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class TransactionDateChanged
    {
        public Guid Id;
        public DateTime TransactionDate;
        public DateTime DueDate;
    }
}
