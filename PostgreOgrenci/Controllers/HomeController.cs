using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PostgreOgrenci.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public ActionResult RTcY()
        {
            return View();
        }
    }
}