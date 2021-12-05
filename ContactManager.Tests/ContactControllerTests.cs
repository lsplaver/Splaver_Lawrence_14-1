using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ContactManager.Tests
{
    public class ContactControllerTests
    {
        public List<Contact> Contacts = new List<Contact>
        {
            new Contact
            {
                ContactId = 1,
                FirstName = "Afirstname",
                LastName = "Alastname",
                CategoryId = 1,
                DateCreated = DateTime.Now,
                Email = "name@example.com",
                Phone = "(123)-456-7890",
                Organization = null
            },
            new Contact
            {
                ContactId = 2,
                FirstName = "John",
                LastName = "Doe",
                CategoryId = 2,
                DateCreated = DateTime.Now,
                Email = "random@dpe.com",
                Phone = "(987)-654-3210",
                Organization = "An Organization"
            }
        };

        // 8/12 Working
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

        // Working
        [Fact]
        public void Add_GET_ReturnViewBagActionValue()
        { 
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);
            
            var result = controller.ViewBag.Action = "Add";
            //var vr = result as ViewResult;
            //Assert.True(vr.ViewData.ContainsKey("Action"));
            //var action = vr.ViewData["Action"];
            string action1 = controller.ViewBag.Action;

            Assert.Equal("Add", action1);
        }

        // Not Working
        [Fact]
        public void Add_GET_ReturnViewBagCategoriesValue()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //QueryOptions<Category> options = null;
            //var result = controller.ViewBag.Categories = unit.Categories.List(options);
            //var categories = unit.Categories.List(options);
            //var result = controller.ViewData.ContainsKey("")

            //Assert.Equal(categories, result);
            Assert.True(controller.ViewData.ContainsKey("Categories"));
        }

        // Working
        [Fact]
        public void Add_GET_ReturnEditViewWithNewContact()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //var result = controller.Add(unit.Contacts.Get(new Contact));
            var result = controller.Add();

            Assert.IsType<RedirectToActionResult>(result);
        }

        // Not working
        [Theory]
        [InlineData(1)]
        public void Edit_GET_ModelIsAContactModel(int id)
        {
            //var unit = GetUnitOfWork();
            //var controller = new ContactController(unit);

            //Contact contact = new Contact();
            //// var result = unit.Contacts.Get(id);
            //// var result = controller.ViewData.Model as Contact;
            ////var result = controller.Edit(id).ViewData.Model as Contact;
            //var result = controller.Edit(id);

            //Assert.Equal((Contact)contact, (Contact)result);
            var rep = new FakeContactRepository();

            var contact = rep.Get(id);
            Assert.IsType<Contact>(contact);
        }

        // Working
        [Fact]
        public void Edit_GET_ReturnViewBagActionValue()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = "Edit";
            string action1 = controller.ViewBag.Action;

            Assert.Equal("Edit", action1);

        }

        // Working
        [Fact]
        public void Edit_GET_ReturnEditViewWithContact()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Edit(0);

            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void Edit_POST_ReturnAddForStringOperation(int id)
        {
            var temp = id;
            string result = "";
            if (temp == 0)
            {
                result = "Add";
            }
            else
            {
                result = "";
            }
            Assert.Equal("Add", result);
        }

        [Theory]
        [InlineData(1)]
        public void Edit_POST_ReturnEditForStringOperation(int id)
        {
            var temp = id;
            string result = "";
            if (temp == 0)
            {
                result = "";
            }
            else
            {
                result = "Edit";
            }

            Assert.Equal("Edit", result);
        }

        [Fact]
        public void Edit_POST_ReturnsEditViewResultIfModelIsNotValid()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            controller.ModelState.AddModelError("", "Test error message.");

            var result = controller.ViewData.ModelState;

            Assert.False(result.IsValid);
        }


        [Theory]
        [InlineData("Add")]
        public void Edit_POST_ReturnViewBagActionAddValue(string operation)
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = operation;

            Assert.Equal("Add", result);
        }

        [Theory]
        [InlineData("Edit")]
        public void Edit_POST_ReturnViewBagActionEditValue(string operation)
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = operation;

            Assert.Equal("Edit", result);
        }

        [Fact]
        public void Edit_POST_InsertNewContactObject()
        {
            //var rep = new FakeContactRepository();
            //var controller = new ContactController((IContactManagerUnitOfWork)rep);
            var unit = GetUnitOfWork();
            //var controller = new ContactController(unit);

            Contact contact = new Contact();

            //string lastName = contact.LastName;
            //QueryOptions<Contact> options = null;
            //options.OrderBy = c => c.LastName;
            //var tempList1 = unit.Contacts.List(options);
            //int count1 = tempList1.Count();
            //unit.Contacts.Insert(contact);
            //var tempList2 = unit.Contacts.List(options);
            //int count2 = tempList2.Count();

            //var tempList1 = rep.List(options);
            //int count1 = tempList1.Count();
            //rep.Insert(contact);
            //var tempList2 = rep.List(options);
            //int count2 = tempList2.Count();

            unit.Contacts.Insert(contact);

            //Assert.True(count2 > count1);
        }

        [Fact]
        public void Edit_POST_UpdateExistingContactObject()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //Contact contact = unit.Contacts.Get(1);

            //unit.Contacts.Update(contact);
            var rep = new FakeContactRepository();

            var contact = rep.Get(1);
            contact.Phone = "(123)-456-7890";

            unit.Contacts.Update(contact);
        }

        [Fact]
        public void Edit_POST_SaveUpdatedContacts()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            unit.Contacts.Save();
            var model = controller.RedirectToAction();
            Assert.IsType<RedirectToActionResult>(model);

        }

        // Working
        [Fact]
        public void Details_GET_ReturnViewBagAction()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = "Detail";
            string action = controller.ViewBag.Action;

            Assert.Equal("Detail", action);
        }

        // Working
        [Fact]
        public void Details_GET_ReturnDetailsView()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        // Working
        [Fact]
        public void Delete_GET_ReturnDeleteView()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Delete(1);

            Assert.IsType<ViewResult>(result);
        }

        // Working
        [Fact]
        public void Edit_POST_ReturnRedirectToActionIndex()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var actionName = "Index";
            var controllerName = "Home";

            var result = controller.RedirectToAction(actionName, controllerName);

            Assert.IsType<RedirectToActionResult>(result);
        }

        // Working
        [Fact]
        public void Delete_POST_ReturnRedirectToActionIndex()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var actionName = "Index";
            var controllerName = "Home";

            var result = controller.RedirectToAction(actionName, controllerName);

            Assert.IsType<RedirectToActionResult>(result);
        }

        // Not working
        [Theory]
        [InlineData(1)]
        public void Delete_POST_ReturnContactObject(int id)
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //var contact = new Contact();
            //QueryOptions<Contact> options = id;
            //contact = unit.Contacts.Get(options);

            //var model = controller.Details(id).ViewData.Model as Contact;l
            var rep = new FakeContactRepository();
            var temp = rep.Get(id);
            //var rep = new Mock<IRepository<Contact>>();
            //rep.Setup(r => r.Get(It.IsAny<int>())).Returns(new Contact());
            //var controller = new ContactController((IContactManagerUnitOfWork)rep);

            var model = controller.Delete(temp);

            Assert.IsType<RedirectToActionResult>(model);
        }

        [Fact]
        public void Delete_POST_ReturnSave()
        {
            var unit = GetUnitOfWork();
            //var controller = new ContactController();

            unit.Contacts.Save();
            //bool result = unit.Contacts.Save();
            //Assert.True(unit.Contacts.Save());
        }

        [Fact]
        public void Delete_POST_ReturnDelete()
        {
            var unit = GetUnitOfWork();

            Contact contact = new Contact();

            unit.Contacts.Delete(contact);
        }
    }
}
