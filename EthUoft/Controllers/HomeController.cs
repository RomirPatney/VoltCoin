using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthUoft.Models;
using EthUoft.Infrastucture;

namespace EthUoft.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Data = APIcaller.All();

            //or you can use APIcaller.Specific(name_of_currency);
            //for a specific currency
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Graph()
        {
            ViewBag.Data = APIcaller.All();

            //or you can use APIcaller.Specific(name_of_currency);
            //for a specific currency
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
