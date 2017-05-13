using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phystones.Models.Account
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Nhập mật khẩu cũ")]
        [MaxLength(50)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu mới")]
        [MaxLength(50)]
        public string NewPassword { get; set; }
        
    }
}