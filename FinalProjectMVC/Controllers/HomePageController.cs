using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels;
using FinalProjectMVC.Data;
using Microsoft.EntityFrameworkCore;

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
                tempUsername = registerViewModel.Username;

                UserPrefs newUserPrefs = new UserPrefs()
                {
                    UsersPrice = registerViewModel.UsersPrice,
                    UsersArea = registerViewModel.UsersArea,
                    UsersCareLevel = registerViewModel.UsersCareLevel
                };

                context.Preferences.Add(newUserPrefs);
                context.SaveChanges();

                User newUser = new User(registerViewModel.Username, registerViewModel.Password, registerViewModel.Email, newUserPrefs);

                //Add new User object to database
                context.Users.Add(newUser);
                context.SaveChanges();

                return Redirect("/Homepage/Index/?id=" + newUser.ID);
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
            if (ModelState.IsValid)
            {
                tempUsername = loginViewModel.Username;

                List<User> existingUsers = context.Users.ToList();

                if (existingUsers != null)
                {
                    foreach (User user in existingUsers)
                    {
                        if (user.Username == loginViewModel.Username && user.Password == loginViewModel.Password)
                        {
                            return Redirect("/HomePage/Index/?id=" + user.ID);
                        }
                    }
                }
            }
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
