using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class HomeController : Controller
    {
        bool State { get; set; } = true;
        bool Role { get; set; } = true;

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


        public ActionResult RegisterFlights()
        {
            ViewData["Req"] = "Registrar Vuelo";
            ViewData["Inicio"] = "si";
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }

        public ActionResult Menu()
        {
            if (TempData["Log-In"] != null && TempData["Role"] != null)
            {
                this.State = (bool)TempData["Log-In"];
                this.Role = (bool)TempData["Role"];
            }
            ViewData["Nombre"] = "Eduard Tomas";
            ViewData["Log-In"] = this.State;
            ViewData["Role"] = this.Role;

            return View();
        }
    }
}
