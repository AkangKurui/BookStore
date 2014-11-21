using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreEvent
{
    public class ItemAdded
    {
        public Guid Id;
        public Guid ItemId;
        public string Name;
        public string Barcode;
        public int Qty;
        public double Price;
        public double Total;
    }
}
