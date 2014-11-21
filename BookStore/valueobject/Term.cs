using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.valueobject
{
    public class Term
    {
        public string Code { get; set; }
        public int Value { get; set; }

        public Term() { }

        public Term(string code, int value)
        {
            // TODO: Complete member initialization
            this.Code = code;
            this.Value = value;
        }

        public static Term Create(string code, int value)
        {
            return new Term(code, value);
        }

        public void ChangeTerm(string code, int value)
        {
            this.Code = code;
            this.Value = value;
        }
    }
}
