using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.command;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Book Store";
            var bookstore = Domain.BookStoreQuery.GetAllInvoice();
            return View(bookstore);
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetInvoice(Guid id)
        {
            var logs = Domain.BookStoreQuery.GetInvoice(id);
            return Json(logs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItem(Guid id)
        {
            var logs = Domain.BookStoreQuery.GetItem(id);
            return Json(logs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItems(Guid id)
        {
            var logs = Domain.BookStoreQuery.GetItems(id);
            return Json(logs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(string name)
        {
            CreateInvoice cmd = new CreateInvoice
            {
                Id = Guid.NewGuid(),
                Customer = new BookStore.entity.Customer(Guid.NewGuid(), name, "Batam", new BookStore.valueobject.Term("001", 15))
            };
            Domain.Dispatcher.SendCommand<CreateInvoice>(cmd);
            return RedirectToAction("");
        }

        [HttpPost]
        public JsonResult ChangeTransactionDate(Guid id, string date)
        {
            var day = Convert.ToInt32(date.Substring(0, 2));
            var month = Convert.ToInt32(date.Substring(3, 2));
            var year = Convert.ToInt32(date.Substring(6, 4));
            ChangeTransactionDate cmd = new ChangeTransactionDate
            {
                Id = id,
                TransactionDate = new DateTime(year, month, day)
            };
            Domain.Dispatcher.SendCommand<ChangeTransactionDate>(cmd);
            var invoice = Domain.BookStoreQuery.GetInvoice(id);
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeCustomer(Guid id, string name, string address, string termCode, int termValue)
        {
            ChangeCustomer cmd = new ChangeCustomer
            {
                Id = id,
                Customer = new BookStore.entity.Customer(Guid.NewGuid(), name, address, new BookStore.valueobject.Term(termCode, termValue))
            };
            Domain.Dispatcher.SendCommand<ChangeCustomer>(cmd);
            return RedirectToAction("");
        }

        [HttpPost]
        public ActionResult ChangeTerm(Guid id, string termCode, int termValue)
        {
            ChangeTerm cmd = new ChangeTerm
            {
                Id = id,
                Code = termCode,
                Value = termValue
            };
            Domain.Dispatcher.SendCommand<ChangeTerm>(cmd);
            return RedirectToAction("");
        }

        [HttpPost]
        public ActionResult ChangeQty(Guid id, string qty, Guid transId)
        {
            ChangeQty cmd = new ChangeQty
            {
                Id = transId,
                ItemId = id,
                Qty = Convert.ToInt32(qty)
            };
            Domain.Dispatcher.SendCommand<ChangeQty>(cmd);
            return RedirectToAction("");
        }

        [HttpDelete]
        public ActionResult DeleteInvoice(Guid id)
        {
            DeleteInvoice cmd = new DeleteInvoice
            {
                Id = id,
            };
            Domain.Dispatcher.SendCommand<DeleteInvoice>(cmd);
            return RedirectToAction("");
        }

        [HttpPost]
        public JsonResult AddItem(Guid id, string name, string barcode, string qty, string price)
        {
            var itemId = Guid.NewGuid();
            AddItem cmd = new AddItem
            {
                Id = id,
                ItemId = itemId,
                Qty = Convert.ToInt32(qty),
                Barcode = barcode,
                Price = Convert.ToDouble(price),
                Name = name,
            };
            Domain.Dispatcher.SendCommand<AddItem>(cmd);
            var items = Domain.BookStoreQuery.GetItem(itemId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
