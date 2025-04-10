using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Btl_web_nc.Models;

public partial class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Tên không được để trống")]
    [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự")]
    public string PassWord { get; set; } = null!;

    public int RoleId { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public TimeOnly? PreferredTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
