using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Phonebook.Controllers;
using Phonebook.Models;
using Phonebook.Services;

namespace Phonebook.UnitTests.Controllers
{
    [TestFixture]
    public class PhonebookControllerTests
    {
        private Mock<IPhonebookService> _service;
        private PhonebookController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IPhonebookService>();
            _controller = new PhonebookController(_service.Object);
        }

        [Test]
        public void Should_list_all_contacts()
        {
            var list = new ContactsViewModel
            {
                Contacts = new List<ContactViewModel>
                {
                    new ContactViewModel { Name = "name1", Address = "address1", PhoneNumber = "phone1" },
                    new ContactViewModel { Name = "name3", Address = "address2", PhoneNumber = "phone2" },
                    new ContactViewModel { Name = "name3", Address = "address3", PhoneNumber = "phone3" },
                }
            };

            _service.Setup(serv => serv.GetContactsAsync().Result).Returns(list);

            var result = _controller.GetContactsAsync(string.Empty, string.Empty).Result;

            Assert.That(result.ViewName, Is.EqualTo("GetContacts"));

            var actual = result.Model as ContactsViewModel;
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Contacts, Is.EquivalentTo(list.Contacts));
        }
    }
}
