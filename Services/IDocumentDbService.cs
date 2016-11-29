using System.Linq;
using System.Threading.Tasks;
using auth0search.Models;

namespace auth0search.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public interface IDocumentDbService
    {
        Task<PagedResults<auth0search.Models.Db.ContactAddress>> GetContactAddresses(string user,int size = 10, string continuationToken = "");
        Task<string> AddContactAddress(auth0search.Models.Db.ContactAddress address);
        Task UpdateContactAddress(auth0search.Models.Db.ContactAddress address);
        auth0search.Models.Db.NotificationPreferences GetNotificationPreferences(string user);
        Task<string> AddNotificationPreferences(auth0search.Models.Db.NotificationPreferences preferences);
        Task UpdateNotificationPreferences(auth0search.Models.Db.NotificationPreferences preferences);
        Task DeleteContactAddress(string id);
    }
}