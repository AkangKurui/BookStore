using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPI.Models
{
    public class AddItemParam
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
    }
}