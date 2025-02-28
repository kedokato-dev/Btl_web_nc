using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class Article
    {
        public Article()
        {
            Topics = new HashSet<Topic>();
        }

        public int ArticleId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
