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

//var _admin = db.ogrenci.Where(s => s.email == ogrenci.email);
//if (_admin.Any())
//{
//    if (_admin.Where(s => s.isim == ogrenci.isim).Any())
//    {

//        return Json(new { status = true, message = "Login Successfull!" });
//    }
//    else
//    {
//        return Json(new { status = false, message = "Invalid Password!" });
//    }
//}
//else
//{
//    return Json(new { status = false, message = "Invalid Email!" });
//}