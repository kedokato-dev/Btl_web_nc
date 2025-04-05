using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string PassWord { get; set; } = null!;

    public int RoleId { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public TimeOnly? PreferredTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
