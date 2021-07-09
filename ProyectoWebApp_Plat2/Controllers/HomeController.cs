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
        bool state { get; set; }
        // Variable que guarda el rol del usuario que ha iniciado sesión; en caso de cerrar sesión o no estar iniciada esta misma, rol tomara el valor de false
        bool role { get; set; }

        // Variables por defecto que debn ir en las casillas de los filtros
        string origen = "Todo";
        string destino = "Todo";
        DateTime fechaDesde = DateTime.Today;
        DateTime fechaHasta = DateTime.Today.AddMonths(6);

        string option;
        string idVueloReserva;
        Customer reserveCustomer;

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
            ViewData["Log-In"] = this.state;
            ViewData["Role"] = this.role;
            return View();
        }

        public async Task<ActionResult> Menu(string origen, string destino, string desde, string hasta)
        {
            if (Session["Log-In"] != null && Session["Role"] != null)
            {
                this.state = (bool)Session["Log-In"];
                this.role = (bool)Session["Role"];
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
            ViewData["Log-In"] = this.state;
            ViewData["Role"] = this.role;

            // Lista que recibira los vuelos desde la API
            List<Flight> vuelos = await GetFlights();

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
            //parametros string Nombres, string Apellidos, string Rut, string Numero_Pasaporte, string Direccion, int Numero_Direccion, int Numero_Telefono, string Nombres_Emergencia, string Apellidos_Emergencia, int Numero_Telefono_Emergencia, int ID
            //this.reserveCustomer = new Customer(Nombres, Apellidos, Rut, Numero_Pasaporte, Direccion, Numero_Direccion, Numero_Telefono, Nombres_Emergencia, Apellidos_Emergencia, Numero_Telefono_Emergencia, ID);

            return View();
        }

        // Métodos que redirigen hacia otra vista

        /// <summary>
        /// Metodo que almacena temporalmente dentro de las etiquetas Log-In y Role valores false de tipo bool,
        /// estos datos seran usados posteriormente en la acción Menu, luego redirige hacia la acción Menu en el controlador Home 
        /// </summary>
        /// <returns>Redirige a la Acción Menu</returns>
        public ActionResult Logout()
        {
            Session["Log-In"] = false;
            Session["Role"] = false;
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

        public ActionResult GoToReserve()
        {
            this.idVueloReserva = Request.QueryString["id"];
            Session["cantidad"] = Request.QueryString["amount"];
            this.option = Request.QueryString["option"];

            return RedirectToAction("ToReserve");
        }

        public FileStreamResult SendVoucher()
        {
            return PdfVoucherWriter.GetInstance().WriteVoucher(this.reserveCustomer);
        }

        /// <summary>
        /// Método que obtiene todos los vuelos de la API
        /// </summary>
        /// <returns>
        /// Retorna todos los vuelos en forma de un list<Flight>
        /// </returns>
        public async Task<List<Flight>> GetFlights()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Flights");
                List<Flight> flight = new List<Flight>();
                if (response.IsSuccessStatusCode)
                {
                    flight = await response.Content.ReadAsAsync<List<Flight>>();
                }

                return flight;
            }
        }

        public async Task<Flight> GetFlight()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Flights");
                Flight flight = null;

                if (response.IsSuccessStatusCode)
                {
                    flight = await response.Content.ReadAsAsync<Flight>();
                }

                return flight;
            }
        }

        public async Task<Customer> GetCustomer()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Customers/");
                Customer customer = null;

                if (response.IsSuccessStatusCode)
                {
                    customer = await response.Content.ReadAsAsync<Customer>();
                }

                return customer;
            }
        }
    }
}