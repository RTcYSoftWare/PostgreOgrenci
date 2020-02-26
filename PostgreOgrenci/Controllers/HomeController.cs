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


        [HttpGet("grid")]
        public ActionResult Grid()
        {
            var data = _ctxpost.ogrenci;

            var grid = new WebGrid(source: data);

            return View(grid);
        }
    }
}