using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginViewModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginViewModel()
        {

        }
    }
}
