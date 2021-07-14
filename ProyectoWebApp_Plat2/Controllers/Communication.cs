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
        public static Communication instance { get; private set; }

        private Communication() { }

        public static Communication GetInstance()
        {
            if(instance == null)
            {
                instance = new Communication();
            }

            return instance;
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

        public async Task<Flight> GetFlight(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Flights/" + id);
                Flight flight = null;

                if (response.IsSuccessStatusCode)
                {
                    flight = await response.Content.ReadAsAsync<Flight>();
                }

                return flight;
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Customers/" + id);
                Customer customer = null;

                if (response.IsSuccessStatusCode)
                {
                    customer = await response.Content.ReadAsAsync<Customer>();
                }

                return customer;
            }
        }

        public async Task<Manager> GetManager(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Managers/" + id);
                Manager manager = null;

                if (response.IsSuccessStatusCode)
                {
                    manager = await response.Content.ReadAsAsync<Manager>();
                }

                return manager;
            }
        }
    }
}
