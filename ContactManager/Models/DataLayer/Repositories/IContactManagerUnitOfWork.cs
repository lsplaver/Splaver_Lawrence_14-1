using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public interface IContactManagerUnitOfWork
    {
        public IRepository<Contact> Contacts { get; }
        public IRepository<Category> Categories { get; }

        public void Save();
    }
}
