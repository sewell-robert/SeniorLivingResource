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

        static public CommunityInfoClass community = new CommunityInfoClass("The Piper", "High", "North", "High");

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            ViewBag.community = community;

            if (id > 0)
            {
                User user = context.Users.Single(c => c.ID == id);

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

        //Take user input as RegisterViewModel and create new User object
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
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
            return View(loginViewModel);
        }

        public IActionResult Browse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Browse(string name, string price, string area, string careLevel)
        {
            return View();
        }
    }
}
