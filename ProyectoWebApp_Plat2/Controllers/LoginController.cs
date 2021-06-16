using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterCustomer()
        {
            return View();
        }


        public ActionResult ToMenu(bool role, string name)
        {
            TempData["Log-In"] = true;
            TempData["Role"] = role;
            TempData["Nombre"] = name;
            return RedirectToAction("Menu","Home");
        }

        public ActionResult ToRegisterCustomer()
        {

            return RedirectToAction("RegisterCustomer", "Login");
        }

        /*
        public void LoginUser() {
            System.Diagnostics.Debug.Write("PROBANDO BOTON LOGIN");

            return;
        }
        */
    }
}
