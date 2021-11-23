using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDUT.Model
{
    public class LoginModels
    {
        [Required]
        public string TenTruyCap { set; get; }
        public string MatKhau { set; get; }
        public string Rememberme { set; get; }
    }
}