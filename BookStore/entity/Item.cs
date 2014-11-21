using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using BookStore.command;
using BookStoreEvent;

namespace BookStore.entity
{
    public class Item
    {
        public Guid Id { get; private set; }
        public Guid ItemId { get; private set; }
        public int Qty { get; private set; }
        public double Total { get; private set; }
        public double Price { get; private set; }

        internal object Handle(AddItem c)
        {
            Id = c.Id;
            ItemId = c.ItemId;
            Qty = c.Qty;
            Price = c.Price;
            Total = CalculateTotal(c.Price, c.Qty);
            return new ItemAdded
            {
                Id = Id,
                ItemId = ItemId,
                Barcode = c.Barcode,
                Name = c.Name,
                Price = c.Price,
                Qty = c.Qty,
                Total = Total
            };
        }

        internal object Handle(ChangeQty c)
        {
            Id = c.Id;
            ItemId = c.ItemId;
            Qty = c.Qty;
            Total = CalculateTotal(this.Price, Qty);
            return new QtyChanged
            {
                Id = Id,
                ItemId = ItemId,
                Qty = Qty,
                Total = Total
            };
        }

        private double CalculateTotal(double price, int qty)
        {
            return price * qty;
        }

        internal void Apply(ItemAdded e)
        {
            this.Id = e.Id;
            this.ItemId = e.ItemId;
            this.Price = e.Price;
            this.Qty = e.Qty;
            this.Total = e.Total;
        }

        internal void Apply(QtyChanged e)
        {
            this.Qty = e.Qty;
            this.Total = e.Total;
        }
    }
}
