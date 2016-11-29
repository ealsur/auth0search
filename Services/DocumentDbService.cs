using System.Linq;
using System.Threading.Tasks;
using auth0search.Extensions;
using auth0search.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;

namespace auth0search.Services
{
    /// <summary>
    /// DocumentDB service
    /// </summary>
    public class DocumentDbService : IDocumentDbService
    {
        private readonly DocumentDbProvider _provider;
        public DocumentDbService(IConfiguration configuration)
        {
            _provider = new DocumentDbProvider(new DocumentDbSettings(configuration));
        }

        /// <summary>
        /// Builds a query for contact addreses
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResults<auth0search.Models.Db.ContactAddress>> GetContactAddresses(string user,int size = 10, string continuationToken = "")
        {
            var feedOptions = new FeedOptions() { MaxItemCount = size };
            if (!string.IsNullOrEmpty(continuationToken))
            {
                feedOptions.RequestContinuation = continuationToken;
            }
            return  await _provider.CreateQuery<auth0search.Models.Db.ContactAddress>(feedOptions).Where(x => x.Type == "address" && x.User == user).ToPagedResults();
        }

        /// <summary>
        /// Adds a contact address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<string> AddContactAddress(auth0search.Models.Db.ContactAddress address)
        {
            return await _provider.AddItem<auth0search.Models.Db.ContactAddress>(address);
        }

        /// <summary>
        /// Updates a contact address
        /// </summary>
        /// <param name="address"></param>
        public async Task UpdateContactAddress(auth0search.Models.Db.ContactAddress address)
        {
            await _provider.UpdateItem<auth0search.Models.Db.ContactAddress>(address, address.Id);
        }

        /// <summary>
        /// Deletes a contact address
        /// </summary>
        /// <param name="address"></param>
        public async Task DeleteContactAddress(string id)
        {
            await _provider.DeleteItem(id);
        }

        /// <summary>
        /// Builds a query for notification preferences
        /// </summary>
        /// <returns></returns>
        public auth0search.Models.Db.NotificationPreferences GetNotificationPreferences(string user)
        {
            var feedOptions = new FeedOptions() { MaxItemCount = 1 };
            return  _provider.CreateQuery<auth0search.Models.Db.NotificationPreferences>(feedOptions).Where(x => x.Type == "notificationpreferences" && x.User == user).TakeOne();
        }

        /// <summary>
        /// Adds notification preferences
        /// </summary>
        /// <param name="preferences"></param>
        /// <returns></returns>
        public async Task<string> AddNotificationPreferences(auth0search.Models.Db.NotificationPreferences preferences)
        {
            return await _provider.AddItem<auth0search.Models.Db.NotificationPreferences>(preferences);
        }

        /// <summary>
        /// Updates notification preferences
        /// </summary>
        /// <param name="preferences"></param>
        public async Task UpdateNotificationPreferences(auth0search.Models.Db.NotificationPreferences preferences)
        {
            await _provider.UpdateItem<auth0search.Models.Db.NotificationPreferences>(preferences, preferences.Id);
        }
    }
}
