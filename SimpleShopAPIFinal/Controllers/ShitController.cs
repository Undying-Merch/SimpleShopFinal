using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleShopAPIFinal.Controllers
{
    public class ShitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
