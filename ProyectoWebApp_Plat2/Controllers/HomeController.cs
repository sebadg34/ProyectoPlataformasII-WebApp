﻿using System;
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
        // Variables por defecto que deben ir en las casillas de los filtros
        string origen = "Todo";
        string destino = "Todo";
        DateTime fechaDesde = DateTime.Today;
        DateTime fechaHasta = DateTime.Today.AddMonths(6);

        public async Task<ActionResult> Menu(string origen, string destino, string fechaDesde, string fechaHasta)
        {

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
            if (fechaDesde != "" && fechaDesde != null)
            {
                fechaDesde.Replace("-", "/");
                fechaDesde = fechaDesde + " 00:00:00";
                this.fechaDesde = Convert.ToDateTime(fechaDesde);
            }
            // Valor por defecto de fecha mínima de despegue
            else
            {
                this.fechaDesde = DateTime.Today;
            }
            // Si se cumple la condición significa que filtros fueron aplicados sobre la fecha máxima de despegue
            if (fechaHasta != "" && fechaHasta != null)
            {
                fechaHasta.Replace("-", "/");
                fechaHasta = fechaHasta + " 23:59:59";
                this.fechaHasta = Convert.ToDateTime(fechaHasta);
            }
            // Valor por defecto de fecha máxima de despegue
            else
            {
                this.fechaHasta = DateTime.Today.AddMonths(6);
            }

            ViewData["Nombre"] = Session["Nombre"] as string;
            ViewData["Login"] = (bool)Session["Login"];
            ViewData["Rol"] = (bool)Session["Rol"];

            // Lista que recibira los vuelos desde la API
            List<Flight> vuelosApi = await Communication.GetInstance().GetFlights();

            // Lista en el que se almacenarán solo los vuelos válidos o con filtros aplicados
            List<Flight> vuelos = new List<Flight>();

            foreach (Flight vuelo in vuelosApi)
            {
                if ((this.origen == "Todo" || this.origen == vuelo.Ciudad_Origen) && (this.destino == "Todo" || this.destino == vuelo.Ciudad_Destino) && (DateTime.Compare(this.fechaDesde, vuelo.Fecha_Salida) < 0) && (DateTime.Compare(this.fechaHasta, vuelo.Fecha_Salida) > 0) && (vuelo.Cupos > 0))
                {
                    vuelos.Add(vuelo);
                }
            }

            ViewBag.vuelosApi = vuelosApi;
            ViewBag.vuelos = vuelos;

            return View();
        }

        public ActionResult RegisterFlights()
        {
            return View();
        }

        /// <summary>
        /// Método que almacena los datos id_vuelo, cantidadPasajeros y usuarioPasajero para ser enviados a la vista ToReserve
        /// </summary>
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
            ViewData["Mensaje"] = "¡Se ha realizado la reserva para " + Session["NombrePasajero"] as string + " en el vuelo " + Session["IdVueloDescriptivo"] as string + " !";
            return View();
        }

        // A partir de aquí se redactan los métodos que redirigen hacia otra acción.

        /// <summary>
        /// Método que almacena datos en las etiquetas Log-In, Role, Nombre e Id para ser usados por el controlador Home en la acción Menu
        /// </summary>
        /// <param name="role"></param>
        /// <param name="name"></param>
        /// <param name="userID"></param>
        /// <returns>Acción Menu del controlador Home</returns>
        public ActionResult ToMenu()
        {
            Session["Login"] = true;
            Session["Rol"] = Convert.ToBoolean(Request.QueryString["rol"]);
            Session["Nombre"] = Request.QueryString["nombre"];
            Session["IdUsuario"] = Request.QueryString["idUsuario"];

            return RedirectToAction("Menu");
        }

        public ActionResult BackToMenu()
        {
            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Metodo que almacena temporalmente dentro de las etiquetas Log-In y Role valores false de tipo bool,
        /// estos datos seran usados posteriormente en la acción Menu, luego redirige hacia la acción Menu en el controlador Home 
        /// </summary>
        /// <returns>Redirige a la Acción Menu</returns>
        public ActionResult Logout()
        {
            Session["Login"] = false;
            Session["Rol"] = false;
            Session["Nombre"] = null;
            Session["IdUsuario"] = null;
            Session["IdVueloReserva"] = null;
            Session["IdVueloDescriptivo"] = null;
            Session["VueloReserva"] = null;
            Session["NombrePasajero"] = null;
            Session["PasajeroReserva"] = null;
            

            return RedirectToAction("Menu");
        }

        /// <summary>
        /// Método que redirige hacia la accion Login que se encuentra en el controlador Login
        /// </summary>
        /// <returns>Acción Login del controlador Login</returns>
        public ActionResult ToLogin()
        {
            return RedirectToAction("Login", "Login");
        }

        public async Task<ActionResult> GoToReserve()
        {
            string idVuelo = Request.QueryString["id"];
            Session["IdVueloReserva"] = idVuelo;
            Flight vueloReserva = await Communication.GetInstance().GetFlight(Int32.Parse(idVuelo));
            Session["VueloReserva"] = vueloReserva;
            Session["IdVueloDescriptivo"] = vueloReserva.ID_Vuelo;
            string opcion = Request.QueryString["opcion"];

            if (opcion == "myself")
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
            string idVuelo = Session["idVueloReserva"] as string;
            string idUsuario = Session["IdUsuario"] as string;
            Customer pasajeroReserva = await Communication.GetInstance().GetCustomer(Int32.Parse(idUsuario));
            Session["PasajeroReserva"] = pasajeroReserva;
            Session["NombrePasajero"] = pasajeroReserva.Nombres + " " + pasajeroReserva.Apellidos;

            await Communication.GetInstance().PostReserve(Int32.Parse(idVuelo), Int32.Parse(idUsuario));
            
            return RedirectToAction("Voucher");
        }

        public async Task<ActionResult> GoToVoucher()
        {
            string idVuelo = Session["idVueloReserva"] as string;
            string idUsuario = Session["IdUsuario"] as string;

            string Nombres = Request.QueryString["Nombres"];
            string Apellidos = Request.QueryString["Apellidos"];
            string Rut = Request.QueryString["Rut"];
            string Numero_Pasaporte = Request.QueryString["Numero_Pasaporte"];
            string Direccion = Request.QueryString["Direccion"];
            int Numero_Direccion = Int32.Parse(Request.QueryString["Numero_Direccion"]);
            int Numero_Telefono = Int32.Parse(Request.QueryString["Numero_Telefono"]);
            string Nombres_Emergencia = Request.QueryString["Nombres_Emergencia"];
            string Apellidos_Emergencia = Request.QueryString["Apellidos_Emergencia"];
            int Numero_Telefono_Emergencia = Int32.Parse(Request.QueryString["Numero_Telefono_Emergencia"]);

            Session["PasajeroReserva"] = new Customer(Nombres, Apellidos, Rut, Numero_Pasaporte, Direccion, Numero_Direccion, Numero_Telefono, Nombres_Emergencia, Apellidos_Emergencia, Numero_Telefono_Emergencia, 0);
            Session["NombrePasajero"] = Nombres + " " + Apellidos;

            await Communication.GetInstance().PostReserve(Int32.Parse(idVuelo), Int32.Parse(idUsuario));

            return RedirectToAction("Voucher");
        }

        public FileStreamResult SendVoucher()
        {
            Flight vueloReserva = (Flight)Session["VueloReserva"];
            Customer pasajeroReserva = (Customer)Session["PasajeroReserva"];
            return PdfVoucherWriter.GetInstance().WriteVoucher(pasajeroReserva, vueloReserva);
        }
   
    }
}