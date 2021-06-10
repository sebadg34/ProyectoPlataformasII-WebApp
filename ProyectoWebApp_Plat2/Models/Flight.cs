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

        public DateTime Llegada { get; set; }

        public DateTime Salida { get; set; }

        public Flight(string Categoria, string Ciudad_Destino, string Ciudad_Origen, int Cupos, int ID, string ID_Vuelo, DateTime Llegada, DateTime Salida)
        {
            this.Categoria = Categoria;
            this.Ciudad_Destino = Ciudad_Destino;
            this.Ciudad_Origen = Ciudad_Origen;
            this.Cupos = Cupos;
            this.ID = ID;
            this.ID_Vuelo = ID_Vuelo;
            this.Llegada = Llegada;
            this.Salida = Salida;
        }
    }
}