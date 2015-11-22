using Microsoft.AspNet.Mvc;
using System;
using TheWorld.ViewModels;
using TheWorld.Services;
using TheWorld;

namespace TheWorldController.Web
{
    public class AppController: Controller
    {
         public IMailService _mailservice { get; }

        public AppController(IMailService service)
        {
            _mailservice = service;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["Data:AppSettings:SiteEmailAdress"];

                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Could not send email");
                }
                if (_mailservice.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email})", model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Send. Thanks";
                }
               
            }
            return View();
        }
    }

}
