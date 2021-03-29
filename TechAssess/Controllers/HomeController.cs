using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TechAssess.Models;

namespace TechAssess.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;
            if (customers == null)
            {
                customers = new List<Customer>();
            }
        }

        public void SaveCache()
        {
            cache["customers"] = customers;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("CustomerList");

        }

        public ActionResult EditCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer, string Id)
        {
            var  customerToEdit = customers.FirstOrDefault(x => x.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customerToEdit.Firstname = customer.Firstname;
                customerToEdit.Lastname = customer.Lastname;
                customerToEdit.SouthAfricanId = customer.SouthAfricanId;
                customerToEdit.MobileNumber = customer.MobileNumber;
                customerToEdit.Email = customer.Email;
                SaveCache();

                return RedirectToAction("CustomerList");
            }
        }

        public ActionResult CustomerList()
        {
            return View(customers);
        }

        public ActionResult DeleteCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        [ActionName("DeleteCustomer")]
        public ActionResult ConfirmDeleteCustomer(string Id)
        {
            Customer customer = customers.FirstOrDefault(x => x.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customers.Remove(customer);
                return RedirectToAction("CustomerList");
            }
        }
    }
}