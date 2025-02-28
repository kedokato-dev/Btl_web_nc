using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models
{
    public partial class Newsletter
    {
        public Newsletter()
        {
            SentMails = new HashSet<SentMail>();
        }

        public int NewsletterId { get; set; }
        public int ServiceId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<SentMail> SentMails { get; set; }
    }
}
