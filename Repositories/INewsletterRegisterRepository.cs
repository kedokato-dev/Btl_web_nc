using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.ViewModel;

namespace Btl_web_nc.Repositories
{
    public interface INewsletterRegisterRepository
    {

        // Task<bool> CheckIfEmailExists(string email);

        Task<IEnumerable<SubscriptionsViewModel>> GetUserSubscriptionsByEmail(string email);
        Task<IEnumerable<Subscription>> GetAllSubscriptions();

        Task AddSubscription(Subscription subscription);
        Task UpdateSubscription(Subscription subscription);
        Task DeleteSubscription(int id);
    
    }
}