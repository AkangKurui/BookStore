using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreReadModels.Models
{
    public class InvoiceReport
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDateString {
            get {
                return TransactionDate.ToString("dd-MM-yyyy");
            }
        }
        public string InvoiceNo { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString
        {
            get
            {
                return DueDate.ToString("dd-MM-yyyy");
            }
        }
        public CustomerReport Customer { get; set; }
        public double SubTotal { get; set; }
        public double NetTotal { get; set; }
    }
}
