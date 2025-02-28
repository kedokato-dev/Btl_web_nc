using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class UserTopicSubscription
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public DateTime? SubscribedAt { get; set; }

        public virtual Topic Topic { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
