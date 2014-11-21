using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.entity;
using BookStore.valueobject;
namespace BookStore.aggregate
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get;set; }
        public string InvoiceNo { get; set; }
        public DateTime DueDate { get; set; }
        public Customer Customer { get; set; }
        public Summary Total { get; set; }

        public Invoice() { }

        public static Invoice Create(Customer cust)
        {
            return new Invoice(cust);
        }

        private Invoice(Customer cust)
        {
            var i = 0;
            this.Id = Guid.NewGuid();
            this.InvoiceNo = "INV-" + i;
            this.TransactionDate = DateTime.Now;
            this.Customer = cust;
            this.DueDate = CalculateDueDate(this.TransactionDate, this.Customer.Term.Value);
            this.Total = new Summary();
        }

        public void ChangeTransactionDate(DateTime dateTime)
        {
            this.TransactionDate = dateTime;
            this.DueDate = CalculateDueDate(dateTime, this.Customer.Term.Value);
        }

        private DateTime CalculateDueDate(DateTime date, int term)
        {
            var dueDate = date.Date.AddDays(term);
            return dueDate;
        }

        public void ChangeTerm(string code, int value)
        {
            this.Customer.Term.ChangeTerm(code, value);
        }
    }
}
