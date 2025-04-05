using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models;

public partial class EmailLog
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int NewsletterId { get; set; }

    public DateTime? SentAt { get; set; }

    public string? Subject { get; set; }

    public string? Status { get; set; }

    public virtual Newsletter Newsletter { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
