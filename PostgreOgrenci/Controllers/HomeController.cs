using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostgreOgrenci.Models;
using System.Web.Helpers;

namespace PostgreOgrenci.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly PostgresContext _ctxpost;

        public HomeController(PostgresContext ctxpost)
        {
            _ctxpost = ctxpost;
        }

        [HttpGet("rtcy")]
        public ActionResult RTcY()
        {
            /*var data = _ctxpost.ogrenci.Min(min => min.isim);
            var data1 = _ctxpost.ogrenci.Min(min => min.soyisim);
            var data2=_ctxpost.ogrenci.Min(min => min.email);

            Ogrenci ogrenci = new Ogrenci();
            ogrenci.isim = data.ToString();
            ogrenci.soyisim = data1.ToString();
            ogrenci.email = data2.ToString();*/

            var ogrenci = _ctxpost.ogrenci.Where(x => x.Id != 5000).ToList();

            return Ok(ogrenci);
        }

        private List<Ogrenci> ogr;

        [HttpGet("grid")]
        public ActionResult Grid()
        {
            var data = _ctxpost.ogrenci;

            ogr = data.ToList<Ogrenci>();
            //ogr.Clear();
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
        [HttpPost("delet")]
        public ActionResult Delet(int delete)
        {
            var data = _ctxpost.ogrenci.Find(delete);
            _ctxpost.ogrenci.Remove(data);
            _ctxpost.SaveChanges();

            var data1 = _ctxpost.ogrenci;
            ogr = data1.ToList<Ogrenci>();

            return View("~/Views/Shared/Grid.cshtml",ogr);
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

            var data1 = _ctxpost.ogrenci;
            ogr = data1.ToList<Ogrenci>();

            return View("~/Views/Shared/Grid.cshtml", ogr);
        }
        public ActionResult Update(String isim, String soyisim, String email)
        {
            //ogrenci ogre = new ogrenci();

            //ogre.isim = isim;
            //ogre.soyisim = soyisim;
            //ogre.email = email;

            //_ctxpost.ogrenci.add(ogre);
            //_ctxpost.savechanges();

            //var data1 = _ctxpost.ogrenci;
            //ogr = data1.tolist<ogrenci>();

            return View("~/Views/Shared/Grid.cshtml", ogr);
        }



    }
}