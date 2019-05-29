﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using smartdressroom.Storage;

namespace smartdressroom.Controllers
{
    public class LoginController : Controller
    {
        public LoginController() { }

        public IActionResult AdminPanel() => View();

        public IActionResult AdminLogin() => View();

        [HttpPost]
        public IActionResult AdminLogin(string login, string password)
        {
            Models.AdminModel admin = null;
            using (var context = new ApplicationContext())
            {
                admin = context.Admins.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
            }
            if (admin != null)
            {
                HttpContext.Session.SetString(nameof(admin), JsonConvert.SerializeObject(admin));
                return RedirectToAction("AdminPanel");
            }
            else return View();
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("admin");
            return RedirectToAction("AdminLogin");
        }
    }
}