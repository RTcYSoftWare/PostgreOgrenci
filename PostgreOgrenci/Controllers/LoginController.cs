using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostgreOgrenci.Models;
using PostgreOgrenci.Services.Abstract;

namespace PostgreOgrenci.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly PostgresContext db;
        private IAdministrator _administrator;

        public LoginController(PostgresContext ctxpost, IAdministrator administrator)
        {
            _administrator = administrator;
            db = ctxpost;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout(OgrenciToken ogrenci)
        {

            return null;
        }

        [HttpPost]
        public ActionResult Validate(Ogrenci ogrenci)
        {

            var user = _administrator.Authenticate(ogrenci.Numara.ToString(),ogrenci.Sifre);
            
            HttpContext.Session.SetString("JWToken", user.Token);
            
            if (user == null)
                return Json(new { status = false, message = "Invalid Password!" });

            return Json(new { status = true, message = "Login Successfull!" });

        }
    }
}