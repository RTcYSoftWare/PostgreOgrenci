using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostgreOgrenci.Models;

namespace PostgreOgrenci.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly PostgresContext db;

        public LoginController(PostgresContext ctxpost)
        {
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

        [HttpPost]
        public ActionResult Validate(Ogrenci admin)
        {
            var _admin = db.ogrenci.Where(s => s.email == admin.email);
            if (_admin.Any())
            {
                if (_admin.Where(s => s.isim == admin.isim).Any())
                {

                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }

        }
    }
}