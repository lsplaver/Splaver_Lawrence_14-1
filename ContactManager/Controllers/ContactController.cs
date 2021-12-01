using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private IContactManagerUnitOfWork data { get; set; }

        public ContactController(IContactManagerUnitOfWork unit)
        {
            data = unit;
        }

        [HttpGet]
        public IActionResult Add()
        {
            this.LoadViewBag("Add");
            return View("Edit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var contact = this.GetContact(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            string operation = contact.ContactId == 0 ? "Add" : "Edit";
            if (ModelState.IsValid)
            {
                if (contact.ContactId == 0)
                {
                    data.Contacts.Insert(contact);
                }
                else
                {
                    data.Contacts.Update(contact);
                }

                data.Contacts.Save();
                return RedirectToAction("Index", "Home");
            }    
            else
            {
                this.LoadViewBag(operation);
                return View(contact);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            this.LoadViewBag("Detail");
            var contact = this.GetContact(id);
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = this.GetContact(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            contact = data.Contacts.Get(contact.ContactId);

            data.Contacts.Delete(contact);
            data.Contacts.Save();
            return RedirectToAction("Index", "Home");
        }

        private Contact GetContact(int id)
        {
            var contactOptions = new QueryOptions<Contact>
            {
                Includes = "Category",
                Where = c => c.ContactId == id
            };

            return data.Contacts.Get(contactOptions);
        }

        private void LoadViewBag(string operation)
        {
            ViewBag.Action = operation;
            ViewBag.Categories = data.Categories.List(new QueryOptions<Category>
            {
                OrderBy = ca => ca.Name
            });
        }
    }
}
