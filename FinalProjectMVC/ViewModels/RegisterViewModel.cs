using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.ViewModels
{
    //Create RegisterViewModel to be able to validate user input in the register form
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Verify Password")]
        public string Verify { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //Create RegisterViewModel constructor to take in arguments
        public RegisterViewModel(string username, string password, string verify, string email)
        {
            Username = username;
            Password = password;
            Verify = verify;
            Email = email;
        }

        public RegisterViewModel()
        {

        }
    }
}
