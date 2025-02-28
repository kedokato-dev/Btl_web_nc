using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class Service
    {
        public Service()
        {
            Newsletters = new HashSet<Newsletter>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;

        public virtual ICollection<Newsletter> Newsletters { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
