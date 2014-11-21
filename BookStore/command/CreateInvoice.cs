using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.entity;

namespace BookStore.command
{
    public class CreateInvoice
    {
        public Guid Id;
        public Customer Customer { get; set; }
    }
}
