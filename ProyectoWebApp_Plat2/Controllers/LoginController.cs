using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Metodo que muestra la vista para iniciar sesion en el sistema.
        /// </summary>
        /// <returns>La vista Login</returns>
        public ActionResult Login()
        {
            return View();
        }
       
        /// <summary>
        /// Metodo que muestra la vista para registrar usuarios al sistema.
        /// </summary>
        /// <returns>La vista RegisterCustomer</returns>
        public ActionResult RegisterCustomer()
        {
            return View();
        } 
    }
}
