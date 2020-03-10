using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostgreOgrenci.Models;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using PostgreOgrenci.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NLog;
using Microsoft.Extensions.Logging;

namespace PostgreOgrenci.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        private readonly PostgresContext _ctxpost;
        private IAdministrator _administrator;
        private List<OgrenciToken> token;
        private List<Ogrenci> ogr;

        public HomeController(PostgresContext ctxpost, IAdministrator administrator, ILogger<HomeController> logger)
        {
            _administrator = administrator;
            _ctxpost = ctxpost;
        }

        [HttpGet("rtcy")]
        public ActionResult RTcY(OgrenciToken ogrenci)
        {
            var data = _ctxpost.ogrenciToken;
            token = data.ToList<OgrenciToken>();

            return View(token);
        }



        [HttpGet("grid")]
        public ActionResult Grid()
        {
            var data = _ctxpost.Ogrenci;
            ogr = data.ToList<Ogrenci>();
            
            return View(ogr);
        }



        [Route("Home/delete")]
        [HttpPost("id")]
        public ActionResult Delet(int delete)
        {
            var data = _ctxpost.Ogrenci.Find(delete);
            _ctxpost.Ogrenci.Remove(data);
            _ctxpost.SaveChanges();

            return RedirectToAction("Grid", "Home");
        }



        [HttpPost("insert")]
        public ActionResult Insert(String isim, String soyisim, String email)
        {
            Ogrenci ogre = new Ogrenci();

            ogre.Adi = isim;
            ogre.Soyadi = soyisim;
            ogre.Email = email;

            _ctxpost.Ogrenci.Add(ogre);
            _ctxpost.SaveChanges();

            return RedirectToAction("Grid", "Home");
        }




        [Route("Home/grid")]
        [HttpPost]
        public ActionResult Update(int update, String isim, String soyisim, String email)
        {
            var updateogr = _ctxpost.Ogrenci.Find(update);
            updateogr.Adi = isim;
            updateogr.Soyadi = soyisim;
            updateogr.Email = email;

            _ctxpost.SaveChanges();

            var data1 = _ctxpost.Ogrenci;
            ogr = data1.ToList<Ogrenci>();

            return View("~/Views/Shared/Grid.cshtml", ogr);
        }
    }
}