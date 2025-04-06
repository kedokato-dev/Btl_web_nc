using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models;

public partial class Article
{
    public int Id { get; set; }

    public int NewsletterId { get; set; }

    public string? Title { get; set; }

    public string? Link { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? Summary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ImageUrl { get; set; } 

    public virtual Newsletter Newsletter { get; set; } = null!;
}
