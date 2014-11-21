using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreCommand
{
    public class CreateInvoice
    {
        public Guid Id;
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public Guid custId;
        public string Name { get; set; }
        public string Address { get; set; }
        public Term Term { get; set; }
    }

    public class Term
    {
        public string Code { get; set; }
        public int Value { get; set; }
    }
}
