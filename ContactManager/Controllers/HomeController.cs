using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private IContactManagerUnitOfWork data { get; set; }

        public HomeController(IContactManagerUnitOfWork unit)
        {
            data = unit;
        }

        public IActionResult Index()
        {
            var contactOptions = new QueryOptions<Contact>
            {
                Includes = "Category",
                OrderBy = c => c.LastName
            };

            return View(data.Contacts.List(contactOptions));
        }
    }
}
