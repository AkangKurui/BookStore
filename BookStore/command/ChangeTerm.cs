using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.command
{
    public class ChangeTerm
    {
        public Guid Id;
        public string Code { get; set; }
        public int Value { get; set; }
    }
}
