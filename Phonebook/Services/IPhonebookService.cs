using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Services
{
    public interface IPhonebookService
    {
        Task<ContactsViewModel> GetContactsAsync();
    }
}