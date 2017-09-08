using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Services;

namespace Phonebook.Controllers
{
    public class PhonebookController : Controller
    {
        private readonly IPhonebookService _service;

        public PhonebookController(IPhonebookService service)
        {
            _service = service;
        }
        
        public async Task<ViewResult> GetContactsAsync(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; ;
            ViewBag.PhoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";
            
            var response = await _service.GetContactsAsync();
            var contacts = response.Contacts;

            if (!string.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    contacts = contacts.OrderByDescending(s => s.Name.Split(' ')[1]).ToList();
                    break;
                case "address":
                    contacts = contacts.OrderBy(s => s.Address).ToList();
                    break;
                case "address_desc":
                    contacts = contacts.OrderByDescending(s => s.Address).ToList();
                    break;
                case "phone":
                    contacts = contacts.OrderBy(s => s.PhoneNumber).ToList();
                    break;
                case "phone_desc":
                    contacts = contacts.OrderByDescending(s => s.PhoneNumber).ToList();
                    break;
                default:
                    contacts = contacts.OrderBy(s => s.Name.Split(' ')[1]).ToList();
                    break;
            }

            return View(contacts);
        }
    }
}
