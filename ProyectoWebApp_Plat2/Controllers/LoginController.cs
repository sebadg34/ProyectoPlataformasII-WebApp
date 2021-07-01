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
using System.Net.Http.Formatting;

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
        
        public async Task<ActionResult> PostLogin(string userEmail, string userPassword) {


            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44350/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          

            var user = new UserModel() { email = userEmail, contrasenia = userPassword};

            HttpResponseMessage response = await client.PostAsJsonAsync("api/login", user);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("success");
            }
            else {

            }
           
            return ToMenu(false,"asd","asd");
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
            TempData["Id"] = idUsuario;
            return RedirectToAction("Menu","Home");
        }

        public ActionResult ToRegisterCustomer()
        {

            return RedirectToAction("RegisterCustomer");
        }

        
    }
}
