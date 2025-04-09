using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;
using Btl_web_nc.ViewModel;

namespace Btl_web_nc.Services
{
    public class NewsletterRegisterServices
    {
        private readonly INewsletterRegisterRepository _newsletterRegisterRepository;
        public NewsletterRegisterServices(INewsletterRegisterRepository newsletterRegisterRepository)
        {
            _newsletterRegisterRepository = newsletterRegisterRepository;
        }

        public async Task<IEnumerable<SubscriptionsViewModel>> GetUserSubscriptionsByEmail(string email)
        {
            var subscriptions = await _newsletterRegisterRepository.GetUserSubscriptionsByEmail(email);
            return subscriptions;
        }
        public async Task<IEnumerable<Subscription>> GetAllSubscriptions()
        {
            var subscriptions = await _newsletterRegisterRepository.GetAllSubscriptions();
            return subscriptions;
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            var subscriptions = await _newsletterRegisterRepository.GetAllSubscriptions();
            return subscriptions.FirstOrDefault(s => s.Id == id);
        }

        public async Task AddSubscription(Subscription subscription)
        {
            await _newsletterRegisterRepository.AddSubscription(subscription);
        }

        public async Task DeleteSubscription(int id)
        {
            await _newsletterRegisterRepository.DeleteSubscription(id);
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            await _newsletterRegisterRepository.UpdateSubscription(subscription);
        }

        

    }
}