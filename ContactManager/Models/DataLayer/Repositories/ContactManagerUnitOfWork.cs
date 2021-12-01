using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactManagerUnitOfWork : IContactManagerUnitOfWork
    {
        private ContactContext context { get; set; }
        public ContactManagerUnitOfWork(ContactContext ctx) => context = ctx;

        private IRepository<Category> categoryData;
        public IRepository<Category> Categories
        {
            get
            {
                if (categoryData == null)
                {
                    categoryData = new Repository<Category>(context);
                }

                return categoryData;
            }
        }

        private IRepository<Contact> contactData;
        public IRepository<Contact> Contacts
        {
            get
            {
                if (contactData == null)
                {
                    contactData = new Repository<Contact>(context);
                }

                return contactData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
