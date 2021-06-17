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

        [HttpPost]
        public ActionResult LoginUser(String userEmail, String userPassword) {

            UserModel user = new UserModel
            {
                email = userEmail,
                contrasenia = userPassword
            };
            
            string apiUrl = "https://localhost:44350/api/Login";
            
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiUrl);
            string userJson = JsonConvert.SerializeObject(user);
            
            
            
           
            
            System.Diagnostics.Debug.Write("PROBANDO BOTON LOGIN " + userEmail + userPassword);
            
            return View();
         
        }
        
    }
}
