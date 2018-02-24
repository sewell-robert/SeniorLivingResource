using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels;
using FinalProjectMVC.DataLayer;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectMVC.Controllers
{
    public class HomePageController : Controller
    {
        static public List<User> users = new List<User>();

        static public CommunityClass community = new CommunityClass("The Piper", "High", "North", "High");

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.community = community;
            return View(users);
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
                User newUser = new User(registerViewModel.Username, registerViewModel.Password, registerViewModel.Email);

                //Add new User object to list of users
                users.Add(newUser);

                return Redirect("/Homepage");
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
            foreach (User user in users)
            {
                if (loginViewModel.Username == user.Username && loginViewModel.Password == user.Password)
                {
                    return Redirect("/Homepage");
                }
            }

            return View(loginViewModel);
        }
    }
}
