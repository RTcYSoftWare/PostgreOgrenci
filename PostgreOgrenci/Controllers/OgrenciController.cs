using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostgreOgrenci.Models;
using PostgreOgrenci.Services.Abstract;

namespace PostgreOgrenci.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class OgrenciController : Controller
    {

        private readonly PostgresContext _ctxpost;
        private IAdministrator _administrator;
        //ILogger<HomeController> _logger;    //new line for nlog
        private readonly ILogger<OgrenciController> _logger;
        private List<OgrenciToken> token;
        private List<Ogrenci> ogr;

        public OgrenciController(PostgresContext ctxpost, IAdministrator administrator, ILogger<OgrenciController> logger)
        {
            _administrator = administrator;
            _ctxpost = ctxpost;
            _logger = logger;
        }

        [HttpGet("Anasayfa")]
        public ActionResult Anasayfa()
        {
            return View();
        }

        //--      Async Çağrıları için Methodlar      --//
        public List<Ogrenci> OgrenciListesi()
        {
            var data = _ctxpost.Ogrenci;
            ogr = data.ToList<Ogrenci>();
            return ogr;
        }

        //--      Async Çağrıları      --//

        [HttpGet("OgrenciListesi")]
        public async Task<ActionResult> Listele()
        {
            Task<List<Ogrenci>> task = new Task<List<Ogrenci>>(OgrenciListesi);
            task.Start();
            return View("Grid", await task);
        }

    }
}