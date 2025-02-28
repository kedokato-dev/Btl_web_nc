using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class Topic
    {
        public Topic()
        {
            UserTopicSubscriptions = new HashSet<UserTopicSubscription>();
            Articles = new HashSet<Article>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; } = null!;

        public virtual ICollection<UserTopicSubscription> UserTopicSubscriptions { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
