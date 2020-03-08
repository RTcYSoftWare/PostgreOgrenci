using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PostgreOgrenci.Controllers
{
    [Route("[controller]")]
    public class OgrenciController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("anasayfa")]
        public ActionResult Anasayfa()
        {
            return View();
        }
    }
}