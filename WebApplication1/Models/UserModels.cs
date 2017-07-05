using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserIdentityModels
    {
        public int Id { get; set; }
        [Required]
       // [RegularExpression()]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string PassWord { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("PassWord", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class UserModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
    }

    public class UserAthModels
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}