using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectMVC.Data;
using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace FinalProjectMVC.Controllers
{
    public class PreferencesController : HomePageController
    {
        private FinalProjectDbContext context;

        public PreferencesController(FinalProjectDbContext dbContext)
            : base(dbContext)
        {
            context = dbContext;
        }

        public IActionResult ChangePrefs()
        {
            PreferencesViewModel preferencesViewModel = new PreferencesViewModel();
            
            return View(preferencesViewModel);
        }

        [HttpPost]
        public IActionResult ChangePrefs(PreferencesViewModel preferencesViewModel)
        {
            if (ModelState.IsValid)
            {
                if (tempUsername != null)
                {
                    User currentUser = context.Users.Single(c => c.Username == tempUsername);

                    UserPrefs currentUserPrefs = context.Preferences.Single(p => p.ID == currentUser.UserPrefsID);

                    currentUserPrefs.UsersPrice = preferencesViewModel.UsersPrice;
                    currentUserPrefs.UsersArea = preferencesViewModel.UsersArea;
                    currentUserPrefs.UsersCareLevel = preferencesViewModel.UsersCareLevel;

                    context.Preferences.Update(currentUserPrefs);
                    context.SaveChanges();

                    return Redirect("/HomePage/Browse");
                }

                //TODO - return an error letting the user know that he needs to login or register first
                string prefsError = "Please login or register to change preferences.";
                ViewBag.error = prefsError;
                ViewBag.n = 3;
                return View(preferencesViewModel);

            }
            return View(preferencesViewModel);
        }
    }
}