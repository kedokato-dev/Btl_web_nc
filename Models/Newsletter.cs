using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models;

public partial class Newsletter
{
    public int Id { get; set; }

    public int TopicId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string RssUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual Topic Topic { get; set; } = null!;
}
