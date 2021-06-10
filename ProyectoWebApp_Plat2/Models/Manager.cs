using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebApp_Plat2.Models
{
    public class Manager
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Rut { get; set; }

        public int ID { get; set; }

        public Manager(string Nombres, string Apellidos, string Rut, int ID)
        {
            this.Nombres = Nombres;
            this.Apellidos = Apellidos;
            this.Rut = Rut;
            this.ID = ID;
        }
    }
}