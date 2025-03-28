﻿using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class User
    {
        public User()
        {
            SentMails = new HashSet<SentMail>();
            Subscriptions = new HashSet<Subscription>();
            UserTopicSubscriptions = new HashSet<UserTopicSubscription>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<SentMail> SentMails { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<UserTopicSubscription> UserTopicSubscriptions { get; set; }
    }
}
