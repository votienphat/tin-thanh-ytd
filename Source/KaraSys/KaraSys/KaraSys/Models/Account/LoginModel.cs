using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KaraSys.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}