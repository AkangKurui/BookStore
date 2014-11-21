using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class CustomerChanged
    {
        public Guid Id;
        public Customer Customer;
        public DateTime DueDate;
    }
}
