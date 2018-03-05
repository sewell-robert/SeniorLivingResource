using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Models
{
    public class User
    {
        //create class to create new users
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserPrefs Prefs { get; set; }
        public int UserPrefsID { get; set; }

        public User(string username, string password, string email, UserPrefs newUserPrefs)
        {
            Username = username;
            Password = password;
            Email = email;
            Prefs = newUserPrefs;
        }

        public User() { }
    }
}
