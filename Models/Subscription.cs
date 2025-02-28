using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class Subscription
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime? SubscribedAt { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
