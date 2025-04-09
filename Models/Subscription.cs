using System.ComponentModel.DataAnnotations;

namespace Btl_web_nc.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public int UserId { get; set; }


    public int NewsletterId { get; set; }


    public string? Frequency { get; set; }

    public DateTime? LastSentAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Newsletter Newsletter { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}