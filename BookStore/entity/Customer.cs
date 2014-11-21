using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.valueobject;

namespace BookStore.entity
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public Term Term { get; private set; }

        public Customer() { }

        public Customer(Guid id, string name, string address, valueobject.Term term)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.Term = term;
        }

        public static Customer Create(Guid id, string name, string address, Term term)
        {
            return new Customer(id, name, address, term);
        }
    }
}
