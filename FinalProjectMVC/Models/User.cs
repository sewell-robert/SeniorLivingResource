using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Models
{
    public class User
    {
        //create class to create new users
        public int UserId;
        private static int nextId = 1;

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;

            UserId = nextId;
            nextId++;
        }
    }
}
