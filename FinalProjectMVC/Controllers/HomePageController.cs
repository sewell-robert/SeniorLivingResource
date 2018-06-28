using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels;
using FinalProjectMVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using MimeKit;
using MailKit.Net.Smtp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectMVC.Controllers
{
    public class HomePageController : Controller
    {
        private FinalProjectDbContext context;

        public HomePageController(FinalProjectDbContext dbContext)
        {
            context = dbContext;
        }

        static public string tempUsername;

        static public Community community = new Community("The Piper", "High", "North Kansas City", "High");

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            ViewBag.community = community;

            if (id > 0)
            {
                User user = context.Users.Single(c => c.ID == id);

                return View(user);
            }

            if (tempUsername != null)
            {
                User user = context.Users.Single(c => c.Username == tempUsername);

                return View(user);
            }

            return View();
        }

        // Method to render register page
        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        //Take user input as RegisterViewModel and create new User object with default UserPrefs object
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                

                //Create an object for the newly registered user's preferences and save changes to the database
                UserPrefs newUserPrefs = new UserPrefs()
                {
                    UsersPrice = registerViewModel.UsersPrice,
                    UsersArea = registerViewModel.UsersArea,
                    UsersCareLevel = registerViewModel.UsersCareLevel
                };

                context.Preferences.Add(newUserPrefs);
                context.SaveChanges();

                //Convert user's password into a PBKDF2 key for strengthened security
                //Create a new User object and add it to the database
                using (var deriveBytes = new Rfc2898DeriveBytes(registerViewModel.Password, 20))
                {
                    byte[] salt = deriveBytes.Salt;
                    byte[] key = deriveBytes.GetBytes(20);

                    User newUser = new User(registerViewModel.Username, key, salt, registerViewModel.Email, newUserPrefs);

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    //Keep track of the user in this session
                    tempUsername = registerViewModel.Username;

                    return Redirect("/Homepage/Index/?id=" + newUser.ID);
                }   
            }

            else return View(registerViewModel);
        }

        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            //Check if user posted valid inputs
            //Get the user's salt from the db
            //Derive a new key and compare with the key/salt stored in the db
            //Keep track of user in session and then redirect to the homepage
            if (ModelState.IsValid)
            {
                List<User> existingUser = context.Users.ToList();

                if (existingUser != null) //TODO - If false, create error message asking user to register first
                {
                    foreach (User user in existingUser)
                    {
                        if ( user.Username == loginViewModel.Username)
                        {
                            byte[] salt = user.Salt;

                            using (var deriveBytes = new Rfc2898DeriveBytes(loginViewModel.Password, salt))
                            {
                                byte[] matchKey = deriveBytes.GetBytes(20);

                                if (matchKey.SequenceEqual(user.HashedPassword))
                                {
                                    tempUsername = loginViewModel.Username;

                                    return Redirect("/HomePage/Index/?id=" + user.ID);
                                }
                            }
                        }        
                    }
                }
            }
            //Send user back to the login page with the following error message
            string loginError = "Username or password is incorrect";
            ViewBag.error = loginError;
            ViewBag.num = 2;
            return View(loginViewModel);
        }

        public IActionResult Browse()
        {
            if (tempUsername != null)
            {
                User currentUser = context.Users.Single(c => c.Username == tempUsername);
                UserPrefs currentUserPrefs = context.Preferences.Single(p => p.ID == currentUser.UserPrefsID);

                string price = currentUserPrefs.UsersPrice;
                string area = currentUserPrefs.UsersArea;
                string careLevel = currentUserPrefs.UsersCareLevel;

                List<Community> communityMatches = new List<Community>();

                List<Community> communities = context.Communities.ToList();

                foreach (Community com in communities)
                {
                    if (com.Price == price && com.Area == area && com.CareLevel == careLevel)
                    {
                        communityMatches.Add(com);
                    }
                }

                ViewBag.number = 1;
                return View(communityMatches);
            }

            List<Community> allCommunities = context.Communities.ToList();

            return View(allCommunities);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SendEmail(ContactViewModel contactViewModel)
        {
            var customerName = contactViewModel.CustomerName;
            var customerEmail = contactViewModel.CustomerEmail;
            var customerRequest = contactViewModel.CustomerRequest;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(customerEmail));
            message.To.Add(new MailboxAddress("sasquatch726@gmail.com"));
            message.Subject = customerName;
            message.Body = new TextPart("plain")
            {
                Text = customerRequest
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("sasquatch726@gmail.com", "HarleyDog627");

                client.Send(message);

                client.Disconnect(true);
            }

            return Redirect("/Homepage/Index/");
        }

        public IActionResult AddCommunity()
        {
            AddCommunityViewModel addCommunityViewModel = new AddCommunityViewModel();

            return View(addCommunityViewModel);
        }

        [HttpPost]
        public IActionResult AddCommunity(AddCommunityViewModel addCommunityViewModel)
        {
            if (ModelState.IsValid)
            {
                List<Community> existingCommunities = context.Communities.ToList();

                foreach (Community community in existingCommunities)
                {
                    if (community.Name == addCommunityViewModel.Name)
                    {
                        community.Description = addCommunityViewModel.Description;
                        community.PhoneNumber = addCommunityViewModel.PhoneNumber;

                        context.Communities.Update(community);
                        context.SaveChanges();

                        return View(addCommunityViewModel);
                    }
                    else
                    {
                        Community newCommunity = new Community
                        {
                            Name = addCommunityViewModel.Name,
                            Description = addCommunityViewModel.Description,
                            PhoneNumber = addCommunityViewModel.PhoneNumber,
                            Price = addCommunityViewModel.Price,
                            Area = addCommunityViewModel.Area,
                            CareLevel = addCommunityViewModel.CareLevel
                        };

                        context.Communities.Add(newCommunity);
                        context.SaveChanges();

                        return View(addCommunityViewModel);

                    }
                }
                
            }

            return Redirect("/Homepage/AddCommunity/");
        }
    }
}
