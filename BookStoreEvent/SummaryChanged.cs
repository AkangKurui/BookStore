using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class SummaryChanged
    {
        public Guid Id;
        public double NetTotal;
        public double SubTotal;
    }
}
