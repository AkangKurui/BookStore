using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreReadModels.Models
{
    public class ItemReport
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
