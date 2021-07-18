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
using System.Text;

namespace ProyectoWebApp_Plat2.Controllers
{
    /// <summary>
    /// Clase que hace manejo de las peticiones a la API dentro de los controladores.
    /// </summary>
    public class Communication
    {
        // Instancia unica de esta clase.
        public static Communication instancia { get; private set; }

        /// <summary>
        /// Constructor de la clase. Es de caracter privado para seguir el patron de disenio Singleton.
        /// </summary>
        private Communication() { }

        /// <summary>
        /// Metodo que verifica si existe o no una instancia de esta clase, de no existir llama al constructor para instanciar una.
        /// </summary>
        /// <returns>Instancia de la clase Communication.</returns>
        public static Communication GetInstance()
        {
            if(instancia == null)
            {
                instancia = new Communication();
            }

            return instancia;
        }

        /// <summary>
        /// Metodo que obtiene todos los vuelos de la API
        /// </summary>
        /// <returns>
        /// Todos los vuelos en forma de un list<Flight>.</returns>
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

        /// <summary>
        /// Metodo que obtiene un vuelo en especifico de la API a traves de su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un vuelo como un objeto de tipo Flight.</returns>
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

        /// <summary>
        /// Metodo que obtiene un cliente en especifico de la API a traves de su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un cliente como un objeto de tipo Customer.</returns>
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

        /// <summary>
        /// Metodo que realiza un Post en la API de una reserva utilizando la id del Vuelo y del Usuario involucrados en la reserva.
        /// </summary>
        /// <param name="idVuelo"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public async Task PostReserve(int idVuelo, int idUsuario)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("https://localhost:44350/");
                clienteHttp.DefaultRequestHeaders.Accept.Clear();
                clienteHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Reserve reserva = new Reserve(idVuelo, idUsuario);
                var jsonSerializado = JsonConvert.SerializeObject(reserva);
                StringContent contenido = new StringContent(jsonSerializado, Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await clienteHttp.PostAsync("api/Reserves/", contenido);
                
            }
        }

    }
}
