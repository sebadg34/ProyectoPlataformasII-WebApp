using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RegisterCustomer()
        {
            return View();
        }

        public ActionResult AdminMenu()
        {
            ViewData["Req"] = "Registrar Vuelo";
            ViewData["Inicio"] = "si";
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }

        public ActionResult AdminFlightList()
        {
            ViewData["Req"] = "Registrar Vuelo";
            ViewData["Inicio"] = "no";
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }

        public ActionResult ClientMenu()
        {
            ViewData["Req"] = "Reservar Vuelo";
            ViewData["Inicio"] = "si";
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }

        public ActionResult CustomerFlightList()
        {
            ViewData["Req"] = "Reservar Vuelo";
            ViewData["Inicio"] = "no";
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }
    }
}