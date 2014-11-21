using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreCommand
{
    public class AddItem
    {
        public Guid Id;
        public Guid ItemId;
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
    }
}
