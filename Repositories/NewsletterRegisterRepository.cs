using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Btl_web_nc.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class NewsletterRegisterRepository : INewsletterRegisterRepository
    {

         private readonly AppDbContext _context;

        public NewsletterRegisterRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubscriptionsViewModel>> GetUserSubscriptionsByEmail(string email)
        {
            var result = await _context.Subscriptions
                .Include(s => s.User) // Nối bảng Subscription với User
                .Include(s => s.Newsletter) // Nối bảng Subscription với Newsletter
                .Where(s => s.User.Email == email) // Điều kiện lọc theo email
                .Select(s => new SubscriptionsViewModel
                {
                    UserName = s.User.Name,
                    NewsletterName = s.Newsletter.Name,
                    Frequency = s.Frequency,
                    UserEmail = s.User.Email
                }) // Map trực tiếp sang SubscriptionsViewModel
                .ToListAsync();

            return result;
        }


        public Task DeleteSubscription(int id)
        {
            var subscription = _context.Subscriptions.Find(id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptions()
        {
            var subscriptions = await _context.Subscriptions.ToListAsync();
            return subscriptions;
        }


        public Task UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            return _context.SaveChangesAsync();
        }

    }

}