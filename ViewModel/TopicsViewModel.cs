using System;
using System.ComponentModel.DataAnnotations;

namespace Btl_web_nc.ViewModels;

public class TopicViewModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Bạn chưa nhập tên.")]
    [StringLength(100, ErrorMessage = "Tên không thể quá 100 ký tự")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Thông tin mô tả không quá 500 ký tự")]
    public string? Description { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Created At")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Is Active")]
    public bool? IsActive { get; set; }
}