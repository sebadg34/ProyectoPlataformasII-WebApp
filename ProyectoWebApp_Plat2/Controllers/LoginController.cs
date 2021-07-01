using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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

        // Métodos que redirigen hacia otra vista

        /// <summary>
        /// Método que almacena temporalmente datos en las etiquetas Log-In, Role, Nombre e Id para ser usados por el controlador Home en la acción Menu
        /// </summary>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="idUsuario"></param>
        /// <returns>Acción Menu del controlador Home</returns>
        public ActionResult ToMenu(bool role, string name, string idUsuario)
        {
            TempData["Log-In"] = true;
            TempData["Role"] = role;
            TempData["Nombre"] = name;
            TempData["Id"] = Convert.ToInt32(idUsuario);

            return RedirectToAction("Menu","Home");
        }

      


    }
}
