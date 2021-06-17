using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebApp_Plat2.Models
{
    public class Flight
    {

        public string Categoria { get; set; }

        public string Ciudad_Destino { get; set; }

        public string Ciudad_Origen { get; set; }

        public int Cupos { get; set; }

        public int ID { get; set; }

        public string ID_Vuelo { get; set; }

        public DateTime Fecha_Llegada { get; set; }

        public DateTime Fecha_Salida { get; set; }

        public Flight(string Categoria, string Ciudad_Destino, string Ciudad_Origen, int Cupos, int ID, string ID_Vuelo, DateTime Fecha_Llegada, DateTime Fecha_Salida)
        {
            this.Categoria = Categoria;
            this.Ciudad_Destino = Ciudad_Destino;
            this.Ciudad_Origen = Ciudad_Origen;
            this.Cupos = Cupos;
            this.ID = ID;
            this.ID_Vuelo = ID_Vuelo;
            this.Fecha_Llegada = Fecha_Llegada;
            this.Fecha_Salida = Fecha_Salida;
        }
    }
}