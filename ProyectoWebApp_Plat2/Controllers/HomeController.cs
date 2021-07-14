using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class HomeController : Controller
    {
        // Variable que guarda el estado de Login, es decir, si se ha iniciado sesión o no
        bool State { get; set; }
        // Variable que guarda el rol del usuario que ha iniciado sesión; en caso de cerrar sesión o no estar iniciada esta misma, rol tomara el valor de false
        bool Role { get; set; }

        // Variables por defecto que deben ir en las casillas de los filtros
        string origen = "Todo";
        string destino = "Todo";
        DateTime fechaDesde = DateTime.Today;
        DateTime fechaHasta = DateTime.Today.AddMonths(6);
        
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

        public ActionResult RegisterFlights()
        {
            ViewData["Nombre"] = "Eduard Tomas";
            ViewData["Log-In"] = State;
            ViewData["Role"] = Role;
            return View();
        }

        public async Task<ActionResult> Menu(string origen, string destino, string desde, string hasta)
        {
            if (Session["Log-In"] != null && Session["Role"] != null)
            {
                State = (bool)Session["Log-In"];
                Role = (bool)Session["Role"];
            }

            // Si origen no es null significa que filtros fueron aplicados sobre la ciudad de origen
            if (origen != null)
            {
                this.origen = origen;
            }
            // Si destino no es null significa que filtros fueron aplicados sobre la ciudad de destino
            if (destino != null)
            {
                this.destino = destino;
            }

            // Si se cumple la condición significa que filtros fueron aplicados sobre la fecha mínima de despegue
            if (desde != "" && desde != null)
            {
                desde.Replace("-", "/");
                desde = desde + " 00:00:00";
                fechaDesde = Convert.ToDateTime(desde);
            }
            // Valor por defecto de fecha mínima de despegue
            else
            {
                fechaDesde = DateTime.Today;
            }
            // Si se cumple la condición significa que filtros fueron aplicados sobre la fecha máxima de despegue
            if (hasta != "" && hasta != null)
            {
                hasta.Replace("-", "/");
                hasta = hasta + " 23:59:59";
                fechaHasta = Convert.ToDateTime(hasta);
            }
            // Valor por defecto de fecha máxima de despegue
            else
            {
                fechaHasta = DateTime.Today.AddMonths(6);
            }

            ViewData["Nombre"] = Session["Nombre"] as string;
            ViewData["Log-In"] = State;
            ViewData["Role"] = Role;

            // Lista que recibira los vuelos desde la API
            List<Flight> vuelos = await Communication.GetInstance().GetFlights();

            // Lista en el que se almacenarán solo los vuelos válidos o con filtros aplicados
            List<Flight> flights = new List<Flight>();

            foreach (Flight vuelo in vuelos)
            {
                if ((this.origen == "Todo" || this.origen == vuelo.Ciudad_Origen) && (this.destino == "Todo" || this.destino == vuelo.Ciudad_Destino) && (DateTime.Compare(this.fechaDesde, vuelo.Fecha_Salida) < 0) && (DateTime.Compare(this.fechaHasta, vuelo.Fecha_Salida) > 0) && (vuelo.Cupos > 0))
                {
                    flights.Add(vuelo);
                }
            }

            ViewBag.vuelos = vuelos;
            ViewBag.flights = flights;

            return View();
        }      

        /// <summary>
        /// Método que almacena los datos id_vuelo, cantidadPasajeros y usuarioPasajero para ser enviados a la vista ToReserve
        /// </summary>
        /// <param name="id_vuelo"></param>
        /// <param name="cantidadPasajeros"></param>
        /// <param name="usuarioPasajero"></param>
        /// <returns>vista ToReserve</returns>
        public ActionResult ToReserve()
        {
            return View();
        }

        /// <summary>
        /// Método que retorna la vista Voucher para acceder
        /// </summary>
        /// <returns></returns>
        public ActionResult Voucher()
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
            Session["Log-In"] = true;
            Session["Role"] = role;
            Session["Nombre"] = name;
            Session["userId"] = idUsuario;

            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Metodo que almacena temporalmente dentro de las etiquetas Log-In y Role valores false de tipo bool,
        /// estos datos seran usados posteriormente en la acción Menu, luego redirige hacia la acción Menu en el controlador Home 
        /// </summary>
        /// <returns>Redirige a la Acción Menu</returns>
        public ActionResult Logout()
        {
            State = false;
            Role = false;

            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Método que redirige hacia la acción Login que se encuentra en el controlador Login
        /// </summary>
        /// <returns>Acción Login del controlador Login</returns>
        public ActionResult ToLogin()
        {
            return RedirectToAction("Login", "Login");
        }

        public async Task<ActionResult> GoToReserve()
        {
            string flighID = Request.QueryString["id"];
            Session["ReserveFlight"] = await Communication.GetInstance().GetFlight(Int32.Parse(flighID));
            string option = Request.QueryString["option"];

            if (option == "myself")
            {
                return RedirectToAction("MyselfVoucher");
            }
            else
            {
                return RedirectToAction("ToReserve");
            }
            
        }

        public async Task<ActionResult> MyselfVoucher()
        {
            string userId = Session["userId"] as string;
            Session["ReserveUser"] = await Communication.GetInstance().GetCustomer(Int32.Parse(userId));
            
            return RedirectToAction("Voucher");
        }

        public ActionResult GoToVoucher()
        {
            //parametros string Nombres, string Apellidos, string Rut, string Numero_Pasaporte, string Direccion, int Numero_Direccion, int Numero_Telefono, string Nombres_Emergencia, string Apellidos_Emergencia, int Numero_Telefono_Emergencia, int ID
            //this.reserveCustomer = new Customer(Nombres, Apellidos, Rut, Numero_Pasaporte, Direccion, Numero_Direccion, Numero_Telefono, Nombres_Emergencia, Apellidos_Emergencia, Numero_Telefono_Emergencia, ID);

            return RedirectToAction("Voucher");
        }

        public FileStreamResult SendVoucher()
        {
            Flight reserveFlight = (Flight)Session["ReserveFlight"];
            Customer reserveCustomer = (Customer)Session["ReserveUser"];
            return PdfVoucherWriter.GetInstance().WriteVoucher(reserveCustomer, reserveFlight);
        }
   
    }
}