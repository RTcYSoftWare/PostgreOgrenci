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

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        private readonly PostgresContext _ctxpost;
        private IAdministrator _administrator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(PostgresContext ctxpost, IAdministrator administrator, ILogger<HomeController> logger)
        {
            _administrator = administrator;
            _ctxpost = ctxpost;
            _logger = logger;
            _logger.LogDebug(1, "Log Debug");
        }

        private List<OgrenciToken> token;

        [HttpGet("rtcy")]
        public ActionResult RTcY(OgrenciToken ogrenci)
        {
            var data = _ctxpost.ogrenciToken;
            token = data.ToList<OgrenciToken>();

            string e;

            try
            {
                int zero = 0;
                int result = 5 / zero;
            }
            catch (DivideByZeroException ex)
            {
                Logger logger = LogManager.GetLogger("*");

                logger.Error(ex, "error");
                LogManager.Shutdown();  // Manually Shuts down the nlog
                e = ex.Message.ToString();
            }

            _logger.LogInformation("Hello, world!");
            return View(token);
        }

        private List<Ogrenci> ogr;

        [AllowAnonymous]
        [HttpGet("grid")]
        public ActionResult Grid()
        {
            var data = _ctxpost.Ogrenci;
            ogr = data.ToList<Ogrenci>(); 
            
            return View(ogr);
        }

        [HttpPost("grid")]
        public ActionResult Grid(int variable)
        {
            var data = _ctxpost.Ogrenci.Find(variable);
            ogr = new List<Ogrenci>();
            ogr.Add(data);

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