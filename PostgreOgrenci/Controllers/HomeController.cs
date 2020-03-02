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

namespace PostgreOgrenci.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly PostgresContext _ctxpost;
        private IAdministrator _administrator;

        public HomeController(PostgresContext ctxpost, IAdministrator administrator)
        {
            _administrator = administrator;
            _ctxpost = ctxpost;
        }

        private List<OgrenciToken> token;

        //[AllowAnonymous]

        [HttpGet("rtcy")]
        public ActionResult RTcY(OgrenciToken ogrenci)
        {
            
            /*var data = _ctxpost.ogrenci.Min(min => min.isim);
            var data1 = _ctxpost.ogrenci.Min(min => min.soyisim);
            var data2=_ctxpost.ogrenci.Min(min => min.email);

            Ogrenci ogrenci = new Ogrenci();
            ogrenci.isim = data.ToString();
            ogrenci.soyisim = data1.ToString();
            ogrenci.email = data2.ToString();*/

            //var ogrenci = _ctxpost.ogrenci.Where(x => x.Id != 5000).ToList();

            //return Ok(ogrenci);
            var data = _ctxpost.ogrenciToken;
            //OgrenciToken ogr = new OgrenciToken();
            //ogr.
            
            token = data.ToList<OgrenciToken>();
            //ogr.Clear();

            //HttpContext.Request.Form[""]

            return View(token);
        }

        private List<Ogrenci> ogr;

        [AllowAnonymous]
        [HttpGet("grid")]
        public ActionResult Grid()
        {
            var data = _ctxpost.ogrenci;
            

            ogr = data.ToList<Ogrenci>();
            //ogr.Clear();

            //HttpContext.Request.Form[""]
            
            return View(ogr);
        }

        [HttpPost("grid")]
        public ActionResult Grid(int variable)
        {
            var data = _ctxpost.ogrenci.Find(variable);
            ogr = new List<Ogrenci>();
            ogr.Add(data);

            return View(ogr);
        }

        [Route("Home/delete")]
        [HttpPost("id")]
        public ActionResult Delet(int delete)
        {
            var data = _ctxpost.ogrenci.Find(delete);
            _ctxpost.ogrenci.Remove(data);
            _ctxpost.SaveChanges();

            //var data1 = _ctxpost.ogrenci;
            //ogr = data1.ToList<Ogrenci>();

            return RedirectToAction("Grid", "Home");

            //return View("~/Views/Shared/Grid.cshtml",ogr);
        }


        [HttpPost("insert")]
        public ActionResult Insert(String isim, String soyisim, String email)
        {
            Ogrenci ogre = new Ogrenci();

            ogre.isim = isim;
            ogre.soyisim = soyisim;
            ogre.email = email;

            _ctxpost.ogrenci.Add(ogre);
            _ctxpost.SaveChanges();

            //var data1 = _ctxpost.ogrenci;
            //ogr = data1.ToList<Ogrenci>();

            return RedirectToAction("Grid", "Home");
            //return View("~/Views/Shared/Grid.cshtml", ogr);
        }

        [Route("Home/grid")]
        [HttpPost]
        public ActionResult Update(int update, String isim, String soyisim, String email)
        {
            var updateogr = _ctxpost.ogrenci.Find(update);
            updateogr.isim = isim;
            updateogr.soyisim = soyisim;
            updateogr.email = email;

            _ctxpost.SaveChanges();

            var data1 = _ctxpost.ogrenci;
            ogr = data1.ToList<Ogrenci>();

            return View("~/Views/Shared/Grid.cshtml", ogr);
        }



    }
}