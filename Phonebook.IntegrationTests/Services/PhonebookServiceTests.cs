using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using Phonebook.Services;

namespace Phonebook.IntegrationTests.Services
{
    [TestFixture]
    public class PhonebookServiceTests
    {
        private HttpClient _client;
        private PhonebookService _service;
        private const string Baseurl = "";

        [SetUp]
        public void SetUp()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _service = new PhonebookService(_client, Baseurl);
        }

        [Test]
        public async void Service_should_get_IsSuccessStatusCode()
        {
            var response = await _service.GetContactsAsync();
            
            Assert.That(response, Is.EqualTo(1));
        }
    }
}