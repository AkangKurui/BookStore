using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.valueobject
{
    public class Summary
    {
        public double NetTotal { get; set; }
        public double SubTotal { get; set; }

        public Summary()
        {
            this.NetTotal = 0;
            this.SubTotal = 0;
        }

        public Summary(double netTotal, double subTotal)
        {
            // TODO: Complete member initialization
            this.NetTotal = netTotal;
            this.SubTotal = subTotal;
        }

        public static Summary ReCalculate(double netTotal, double subTotal)
        {
            return new Summary(netTotal, subTotal);
        }
    }
}
