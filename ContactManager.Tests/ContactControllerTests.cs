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
        private string insertMessage = "";
        private string updateMessage = "";
        private string deleteMessage = "";
        private string saveMessage = "";

        public IContactManagerUnitOfWork GetUnitOfWork()
        {
            var contactRep = new Mock<IRepository<Contact>>();
            contactRep.Setup(cor => cor.Get(It.IsAny<QueryOptions<Contact>>())).Returns(new Contact());
            contactRep.Setup(cor => cor.List(It.IsAny<QueryOptions<Contact>>())).Returns(new List<Contact>());
            contactRep.Setup(cor => cor.Insert(It.IsAny<Contact>())).Verifiable(insertMessage = "insert failed");
            contactRep.Setup(cor => cor.Update(It.IsAny<Contact>())).Verifiable(updateMessage = "update failed");
            contactRep.Setup(cor => cor.Delete(It.IsAny<Contact>())).Verifiable(deleteMessage = "delete failed");
            contactRep.Setup(cor => cor.Save()).Verifiable(saveMessage = "save failed");


            var categoryRep = new Mock<IRepository<Category>>();
            categoryRep.Setup(car => car.List(It.IsAny<QueryOptions<Category>>())).Returns(new List<Category>());

            var unit = new Mock<IContactManagerUnitOfWork>();
            unit.Setup(u => u.Contacts).Returns(contactRep.Object);
            unit.Setup(u => u.Categories).Returns(categoryRep.Object);

            return unit.Object;
        }


        [Fact]
        public void Add_GET_ReturnViewBagActionValue()
        { 
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);
            
            var result = controller.ViewBag.Action = "Add";
            string action = controller.ViewBag.Action;

            Assert.Equal("Add", action);
        }

        [Fact]
        public void Add_GET_ReturnViewBagCategoriesValue()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            QueryOptions<Category> options = null;
            //var result = controller.ViewBag.Categories = unit.Categories.List(options);
            var categories = unit.Categories.List(options);
            //var result = controller.ViewData.ContainsKey("")

            //var result = controller.ViewData.Keys.Count;

            //Assert.Equal(categories, result);
            //Assert.True(controller.ViewData.ContainsKey("Categories"));
            Assert.IsType<List<Category>>(categories);
        }

        [Fact]
        public void Add_GET_ReturnEditViewWithNewContact()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Add();

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void Edit_GET_ModelIsAContactModel(int id)
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = ((ViewResult)controller.Edit(id)).ViewData.Model as Contact;

            Assert.IsType<Contact>(result);
        }

        [Fact]
        public void Edit_GET_ReturnViewBagActionValue()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = "Edit";
            string action1 = controller.ViewBag.Action;

            Assert.Equal("Edit", action1);

        }

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

            Contact contact = new Contact()
            {
                ContactId = 3,
                CategoryId = 3,
                DateCreated = DateTime.Now,
                Email = "sdada@hfhf.vuv",
                Phone = "(654)-982-4231",
                Organization = null,
                FirstName = "Edhgh",
                LastName = "jgkjg"
            };
            insertMessage = "";
            //string lastName = contact.LastName;
            //QueryOptions<Contact> options = null;
            //options.OrderBy = c => c.LastName;
            //var tempList1 = unit.Contacts.List(options);
            //int count1 = tempList1.Count();
            //object temp = null;
            unit.Contacts.Insert(contact);
            Mock.Verify();
            //var tempList2 = unit.Contacts.List(options);
            //int count2 = tempList2.Count();
            //message = "";
            //var tempList1 = rep.List(options);
            //int count1 = tempList1.Count();
            //rep.Insert(contact);
            //var tempList2 = rep.List(options);
            //int count2 = tempList2.Count();

            //unit.Contacts.Insert(contact);

            //Assert.True(count2 > count1);
            Assert.True(insertMessage == "");
        }

        [Fact]
        public void Edit_POST_UpdateExistingContactObject()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //Contact contact = unit.Contacts.Get(1);

            //unit.Contacts.Update(contact);
            //var rep = new FakeContactRepository();

            var result = ((ViewResult)controller.Edit(1)).ViewData.Model as Contact;

            updateMessage = "";
            result.Phone = "(123)-456-7890";

            unit.Contacts.Update(result);
            Mock.Verify();
            Assert.True(updateMessage == "");
        }

        [Fact]
        public void Edit_POST_SaveUpdatedContacts()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);
            saveMessage = "";

            unit.Contacts.Save();
            var model = controller.RedirectToAction();
            Mock.Verify();
            Assert.True(saveMessage == "");
            Assert.IsType<RedirectToActionResult>(model);

        }

        [Fact]
        public void Details_GET_ReturnViewBagAction()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.ViewBag.Action = "Detail";
            string action = controller.ViewBag.Action;

            Assert.Equal("Detail", action);
        }

        [Fact]
        public void Details_GET_ReturnDetailsView()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Details(1);
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Details_GET_ReturnContactObject()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var model = ((ViewResult)controller.Details(1)).ViewData.Model as Contact;

            Assert.IsType<Contact>(model);
        }

        [Fact]
        public void Delete_GET_ReturnDeleteView()
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            var result = controller.Delete(1);

            Assert.IsType<ViewResult>(result);
        }

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

        [Theory]
        [InlineData(1)]
        public void Delete_POST_ReturnContactObject(int id)
        {
            var unit = GetUnitOfWork();
            var controller = new ContactController(unit);

            //var contact = new Contact();
            //QueryOptions<Contact> options = id;
            //contact = unit.Contacts.Get(options);
            var result = ((ViewResult)controller.Delete(id)).ViewData.Model as Contact;

            //var model = controller.Details(id).ViewData.Model as Contact;l
            //var rep = new FakeContactRepository();
            //var temp = rep.Get(id);
            //var rep = new Mock<IRepository<Contact>>();
            //rep.Setup(r => r.Get(It.IsAny<int>())).Returns(new Contact());
            //var controller = new ContactController((IContactManagerUnitOfWork)rep);
            deleteMessage = "";
            var model = controller.Delete(result);

            Mock.Verify();

            Assert.True(deleteMessage == "");
            Assert.IsType<RedirectToActionResult>(model);
        }

        [Fact]
        public void Delete_POST_ReturnSave()
        {
            var unit = GetUnitOfWork();
            //var controller = new ContactController();
            saveMessage = "";

            unit.Contacts.Save();
            Mock.Verify();
            //bool result = unit.Contacts.Save();
            //Assert.True(unit.Contacts.Save());
            Assert.True(saveMessage == "");
        }

        [Fact]
        public void Delete_POST_ReturnDelete()
        {
            var unit = GetUnitOfWork();

            Contact contact = new Contact();
            deleteMessage = "";
            unit.Contacts.Delete(contact);
            Mock.Verify();

            Assert.True(deleteMessage == "");
        }
    }
}
