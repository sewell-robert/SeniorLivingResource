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
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
        public string Email { get; set; }

        public UserPrefs Prefs { get; set; }
        public int UserPrefsID { get; set; }

        public User(string username, byte[] hashedPassword, byte[] salt, string email, UserPrefs newUserPrefs)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Salt = salt;
            Email = email;
            Prefs = newUserPrefs;
        }

        public User() { }

        internal User Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
