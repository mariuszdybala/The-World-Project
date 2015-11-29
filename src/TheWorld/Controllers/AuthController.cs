using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using TheWorld.Models;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }


        // GET: /<controller>/
        public IActionResult Login()
        {
            //if(User.Identity.IsAuthenticated)
            if(true)
            {
                // Jeżeli użytkownik jest zalogowany to od razu leci na strone z trasami !
                return RedirectToAction("Trips", "App");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                        return RedirectToAction(ReturnUrl);
                }
                else
                    ModelState.AddModelError("", "Username of Password incorrect");
             }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
