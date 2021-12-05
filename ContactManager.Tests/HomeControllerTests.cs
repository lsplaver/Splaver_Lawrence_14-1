using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace ContactManager.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            var unit = new Mock<IContactManagerUnitOfWork>();
            var contacts = new Mock<IRepository<Contact>>();
            var categories = new Mock<IRepository<Category>>();
            unit.Setup(u => u.Contacts).Returns(contacts.Object);
            unit.Setup(u => u.Categories).Returns(categories.Object);

            var controller = new HomeController(unit.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
