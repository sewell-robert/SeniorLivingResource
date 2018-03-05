using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class PreferencesController : Controller
    {
        public IActionResult AddPrefs()
        {
            PreferencesViewModel preferencesViewModel = new PreferencesViewModel();
            
            return View(preferencesViewModel);
        }

        [HttpPost]
        public IActionResult AddPrefs(PreferencesViewModel preferencesViewModel)
        {
            if (ModelState.IsValid)
            {
                UserPrefs newUserPrefs = new UserPrefs()
                {
                    UsersPrice = preferencesViewModel.UsersPrice,
                    UsersArea = preferencesViewModel.UsersArea,
                    UsersCareLevel = preferencesViewModel.UsersCareLevel
                };

                return Redirect("/HomePage/Browse");

            }

            return View(preferencesViewModel);
        }
    }
}