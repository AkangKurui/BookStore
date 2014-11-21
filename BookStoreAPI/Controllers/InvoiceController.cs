using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookStore.command;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers
{
    //[EnableCors(origins: "http://localhost:3974", headers: "Accept, Origin, Content-type", methods: "POST,PUT,DELETE")]
    public class InvoiceController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var invoices = Domain.BookStoreQuery.GetAllInvoice();
            if (invoices.Count.Equals(0))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Invoice is not found");
            return Request.CreateResponse(HttpStatusCode.OK, invoices);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            if (invoice == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Invoice is not found");
            return Request.CreateResponse(HttpStatusCode.OK, invoice);
        }

        [Route("api/invoice/{id}/item/{itemId}")]
        public HttpResponseMessage GetItem(Guid itemId)
        {
            var item = Domain.BookStoreQuery.GetItem(itemId);
            if (item == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Invoice is not found");
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [Route("api/invoice/{id}/items")]
        public HttpResponseMessage GetItems(Guid id)
        {
            var items = Domain.BookStoreQuery.GetItems(id);
            if (items.Equals(0))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Invoice is not found");
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpPost]
        public HttpResponseMessage Post(CreateInvoiceParam param)
        {
            var id = Guid.NewGuid();
            CreateInvoice cmd = new CreateInvoice
            {
                Id = id,
                Customer = new BookStore.entity.Customer(Guid.NewGuid(), param.Name, "batam", new BookStore.valueobject.Term("001", 15))
            };
            Domain.Dispatcher.SendCommand<CreateInvoice>(cmd);
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            var response = Request.CreateResponse(HttpStatusCode.Created, invoice.Id);
            return response;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            DeleteInvoice cmd = new DeleteInvoice
            {
                Id = id,
            };
            Domain.Dispatcher.SendCommand<DeleteInvoice>(cmd);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(Guid id, ChangeDateParam param)
        {
            var day = Convert.ToInt32(param.Date.Substring(0, 2));
            var month = Convert.ToInt32(param.Date.Substring(3, 2));
            var year = Convert.ToInt32(param.Date.Substring(6, 4));
            ChangeTransactionDate cmd = new ChangeTransactionDate
            {
                Id = id,
                TransactionDate = new DateTime(year, month, day)
            };
            Domain.Dispatcher.SendCommand<ChangeTransactionDate>(cmd);
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, invoice);
            return response;
        }

        public HttpResponseMessage Put(Guid id, string name, string address, string termCode, int termValue)
        {
            ChangeCustomer cmd = new ChangeCustomer
            {
                Id = id,
                Customer = new BookStore.entity.Customer(Guid.NewGuid(), name, address, new BookStore.valueobject.Term(termCode, termValue))
            };
            Domain.Dispatcher.SendCommand<ChangeCustomer>(cmd);
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, invoice);
            return response;
        }

        public HttpResponseMessage Put(Guid id, string termCode, int termValue)
        {
            ChangeTerm cmd = new ChangeTerm
            {
                Id = id,
                Code = termCode,
                Value = termValue
            };
            Domain.Dispatcher.SendCommand<ChangeTerm>(cmd);
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, invoice);
            return response;
        }

        public HttpResponseMessage Post(Guid id, AddItemParam param)
        {
            var itemId = Guid.NewGuid();
            AddItem cmd = new AddItem
            {
                Id = id,
                ItemId = itemId,
                Barcode = param.Barcode,
                Name = param.Name,
                Qty = param.Qty,
                Price = param.Price
            };
            Domain.Dispatcher.SendCommand<AddItem>(cmd);
            var item = Domain.BookStoreQuery.GetItem(itemId);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            return response;
        }

        [HttpPut]
        [Route("api/invoice/{id}/item/{itemId}")]
        public HttpResponseMessage Put(Guid id, Guid itemId, string qty)
        {
            ChangeQty cmd = new ChangeQty
            {
                Id = id,
                ItemId = itemId,
                Qty = Convert.ToInt32(qty),
            };
            Domain.Dispatcher.SendCommand<ChangeQty>(cmd);
            var item = Domain.BookStoreQuery.GetItem(itemId);
            var response = Request.CreateResponse(HttpStatusCode.OK, item);
            return response;
        }
    }
}
