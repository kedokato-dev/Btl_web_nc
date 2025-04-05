using System;
using System.Collections.Generic;

namespace Btl_web_nc.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Newsletter> Newsletters { get; set; } = new List<Newsletter>();
}
