using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class QtyChanged
    {
        public Guid Id;
        public Guid ItemId;
        public int Qty;
        public double Total;
    }
}
