using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProyectoWebApp_Plat2.Controllers
{
    /// <summary>
    /// Clase que hace manejo de las peticiones a la API dentro de los controladores.
    /// </summary>
    public class Communication
    {
        public static Communication instancia { get; private set; }

        private Communication() { }

        public static Communication GetInstance()
        {
            if(instancia == null)
            {
                instancia = new Communication();
            }

            return instancia;
        }

        /// <summary>
        /// Método que obtiene todos los vuelos de la API
        /// </summary>
        /// <returns>
        /// Retorna todos los vuelos en forma de un list<Flight>
        /// </returns>
        public async Task<List<Flight>> GetFlights()
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("https://localhost:44350/");
                clienteHttp.DefaultRequestHeaders.Accept.Clear();
                clienteHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await clienteHttp.GetAsync("api/Flights");
                List<Flight> vuelos = new List<Flight>();
                if (respuesta.IsSuccessStatusCode)
                {
                    vuelos = await respuesta.Content.ReadAsAsync<List<Flight>>();
                }

                return vuelos;
            }
        }

        public async Task<Flight> GetFlight(int id)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("https://localhost:44350/");
                clienteHttp.DefaultRequestHeaders.Accept.Clear();
                clienteHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await clienteHttp.GetAsync("api/Flights/" + id);
                Flight vuelo = null;

                if (respuesta.IsSuccessStatusCode)
                {
                    vuelo = await respuesta.Content.ReadAsAsync<Flight>();
                }

                return vuelo;
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("https://localhost:44350/");
                clienteHttp.DefaultRequestHeaders.Accept.Clear();
                clienteHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await clienteHttp.GetAsync("api/Customers/" + id);
                Customer cliente = null;

                if (respuesta.IsSuccessStatusCode)
                {
                    cliente = await respuesta.Content.ReadAsAsync<Customer>();
                }

                return cliente;
            }
        }
    }
}
