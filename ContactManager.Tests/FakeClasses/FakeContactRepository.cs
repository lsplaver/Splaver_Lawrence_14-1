using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Tests
{
    class FakeContactRepository : IRepository<Contact>
    {
        public void Delete(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Contact Get(QueryOptions<Contact> options)
        {
            return new Contact();
        }

        public Contact Get(int id)
        {
            return new Contact();
        }

        public void Insert(Contact entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> List(QueryOptions<Contact> options)
        {
            //return new List<Contact>(c => c.LastName);
            //return options.OrderBy = c => c.LastName;
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}
