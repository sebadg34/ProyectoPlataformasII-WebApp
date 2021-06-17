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
        bool State { get; set; } = true;
        bool Role { get; set; } = false;

        string origen = "Todo";
        string destino = "Todo";
        DateTime fecha_desde = DateTime.Today;
        DateTime fecha_hasta = DateTime.Today.AddMonths(6);

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
            ViewData["Log-In"] = this.State;
            ViewData["Role"] = this.Role;
            ViewData["Nombre"] = "Eduard Tomas";
            return View();
        }

        public async Task<ActionResult> Menu(string origen, string destino, string desde, string hasta)
        {
            if (TempData["Log-In"] != null && TempData["Role"] != null)
            {
                this.State = (bool)TempData["Log-In"];
                this.Role = (bool)TempData["Role"];
            }

            if (origen != null)
            {
                this.origen = origen;
            }

            if (destino != null)
            {
                this.destino = destino;
            }

            if (desde != "" && desde != null)
            {
                desde.Replace("-", "/");
                desde = desde + " 00:00:00";
                fecha_desde = Convert.ToDateTime(desde);
            }
            else
            {
                fecha_desde = DateTime.Today;
            }

            if (hasta != "" && hasta != null)
            {
                hasta.Replace("-", "/");
                hasta = hasta + " 23:59:59";
                fecha_hasta = Convert.ToDateTime(hasta);
            }
            else
            {
                fecha_hasta = DateTime.Today.AddMonths(6);
            }

            ViewData["Nombre"] = "Eduard Tomas";
            ViewData["Log-In"] = this.State;
            ViewData["Role"] = this.Role;

            List<Flight> vuelos = await GetFlights();
            List<Flight> flights = new List<Flight>();

            foreach (Flight vuelo in vuelos)
            {
                if ((this.origen == "Todo" || this.origen == vuelo.Ciudad_Origen) && (this.destino == "Todo" || this.destino == vuelo.Ciudad_Destino) && (DateTime.Compare(this.fecha_desde, vuelo.Fecha_Salida) < 0) && (DateTime.Compare(this.fecha_hasta, vuelo.Fecha_Salida) > 0))
                {
                    flights.Add(vuelo);
                }
            }

            ViewBag.vuelos = vuelos;
            ViewBag.flights = flights;

            return View();
        }

        public async Task<List<Flight>> GetFlights()
        {
            using(var client = new HttpClient())
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
                else
                {

                }
                return flight;
            }
        }

        public string mirar(string id_vuelo)
        {
            return "ID Vuelo: " + id_vuelo;
        }
    }
}
