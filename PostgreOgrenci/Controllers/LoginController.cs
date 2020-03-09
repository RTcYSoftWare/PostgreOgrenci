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
    [Route("[controller]")]
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

        //[HttpPost("logout")]
        //public ActionResult Logout()
        //{
        //    HttpContext.Session.SetString("JWToken", "");
        //    return RedirectToAction("Login", "Login");
        //}

        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Validate(Ogrenci ogrenci)
        {

            var user = _administrator.Authenticate(ogrenci.Numara, ogrenci.Sifre);
            
            if (user == null)
            {
                return Json(new { status = false, message = "HATALI GİRİŞ" }); 
            }
            else
            {
                
                HttpContext.Session.SetString("JWToken", user.Token);
                return Json(new { status = true, message = "Login Successfull!" });

            }

        }
    }
}

