using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreReadModels.Models
{
    public class CustomerReport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TermReport Term { get; set; }
    }

    public class TermReport 
    {
        public string Code { get; set; }
        public int Value { get; set; }
    }
}
