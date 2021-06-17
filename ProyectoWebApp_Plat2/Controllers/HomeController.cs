using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;

namespace ProyectoWebApp_Plat2.Controllers
{
    public class HomeController : Controller
    {
        bool State { get; set; } = true;
        bool Role { get; set; } = true;

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
            ViewData["Nombre"] = "Eduard Tomas";
            ViewData["Log-In"] = this.State;
            ViewData["Role"] = this.Role;
            return View();
        }

        public ActionResult Menu(string origen, string destino, string desde, string hasta)
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


            Flight vuelo1 = new Flight("Basico", "Santiago", "Antofagasta", 30, 1, "001", new DateTime(2020, 01, 22, 20, 20, 20), new DateTime(2020, 01, 20, 20, 20, 20));
            Flight vuelo2 = new Flight("Normal", "Antofagasta", "Coquimbo", 40, 2, "002", new DateTime(2021, 07, 22, 20, 20, 20), new DateTime(2021, 07, 20, 20, 20, 20));
            Flight vuelo3 = new Flight("Premium", "Antofagasta", "Chiloe", 35, 3, "003", new DateTime(2021, 09, 22, 20, 20, 20), new DateTime(2021, 09, 20, 20, 20, 20));
            Flight vuelo4 = new Flight("Basico", "Concepcion", "Antofagasta", 35, 4, "004", new DateTime(2021, 09, 22, 20, 20, 20), new DateTime(2021, 09, 20, 20, 20, 20));
            Flight vuelo5 = new Flight("Basico", "Santiago", "Antofagasta", 30, 1, "001", new DateTime(2020, 01, 22, 20, 20, 20), new DateTime(2020, 01, 20, 20, 20, 20));
            Flight vuelo6 = new Flight("Normal", "Antofagasta", "Coquimbo", 40, 2, "002", new DateTime(2021, 07, 22, 20, 20, 20), new DateTime(2021, 07, 20, 20, 20, 20));
            Flight vuelo7 = new Flight("Premium", "Antofagasta", "Chiloe", 35, 3, "003", new DateTime(2021, 09, 22, 20, 20, 20), new DateTime(2021, 09, 20, 20, 20, 20));
            Flight vuelo8 = new Flight("Basico", "Concepcion", "Antofagasta", 35, 4, "004", new DateTime(2021, 09, 21, 19, 20, 20), new DateTime(2021, 09, 20, 20, 20, 20));
            Flight vuelo9 = new Flight("Basico", "Arica", "Puerto Montt", 35, 4, "004", new DateTime(2021, 12, 12, 20, 20, 20), new DateTime(2021, 12, 11, 20, 20, 20));

            List<Flight> vuelos = new List<Flight>();

            vuelos.Add(vuelo1);
            vuelos.Add(vuelo2);
            vuelos.Add(vuelo3);
            vuelos.Add(vuelo4);
            vuelos.Add(vuelo5);
            vuelos.Add(vuelo6);
            vuelos.Add(vuelo7);
            vuelos.Add(vuelo8);
            vuelos.Add(vuelo9);

            List<Flight> flights = new List<Flight>();

            foreach (Flight vuelo in vuelos)
            {
                if ((this.origen == "Todo" || this.origen == vuelo.Ciudad_Origen) && (this.destino == "Todo" || this.destino == vuelo.Ciudad_Destino) && (DateTime.Compare(this.fecha_desde, vuelo.Salida) < 0) && (DateTime.Compare(this.fecha_hasta, vuelo.Salida) > 0))
                {
                    flights.Add(vuelo);
                }
            }

            ViewBag.vuelos = vuelos;
            ViewBag.flights = flights;

            return View();
        }

        public string mirar(string id_vuelo)
        {
            return "ID Vuelo: " + id_vuelo;
        }
    }
}
