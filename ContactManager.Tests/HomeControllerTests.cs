using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContactManager.Tests
{
    public class HomeControllerTests
    {
        public IContactManagerUnitOfWork GetUnitOfWork()
        {
            var contactRep = new Mock<IRepository<Contact>>();
            contactRep.Setup(cor => cor.Get(It.IsAny<QueryOptions<Contact>>())).Returns(new Contact());
            contactRep.Setup(cor => cor.List(It.IsAny<QueryOptions<Contact>>())).Returns(new List<Contact>());


            var categoryRep = new Mock<IRepository<Category>>();
            categoryRep.Setup(car => car.List(It.IsAny<QueryOptions<Category>>())).Returns(new List<Category>());

            var unit = new Mock<IContactManagerUnitOfWork>();
            unit.Setup(u => u.Contacts).Returns(contactRep.Object);
            unit.Setup(u => u.Categories).Returns(categoryRep.Object);

            return unit.Object;
        }

        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            var unit = GetUnitOfWork();
            var controller = new HomeController(unit);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_GET_ReturnContactList()
        {
            var unit = GetUnitOfWork();
            var controller = new HomeController(unit);

            QueryOptions<Contact> options = null;

            var contacts = unit.Contacts.List(options);

            Assert.IsType<List<Contact>>(contacts);
        }
    }
}
