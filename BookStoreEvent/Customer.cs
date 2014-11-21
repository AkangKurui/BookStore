using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class Customer
    {
        public Guid custId;
        public string Name;
        public string Address;
        public Term Term;
    }

    public class Term
    {
        public string Code;
        public int Value;
    }
}
