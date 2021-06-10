using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebApp_Plat2.Models
{
    public class Customer
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Rut { get; set; }

        public string Numero_Pasaporte { get; set; }

        public string Direccion { get; set; }

        public int Numero_Direccion { get; set; }

        public int Numero_Telefono { get; set; }

        public string Nombres_Emergencia { get; set; }

        public string Apellidos_Emergencia { get; set; }

        public int Numero_Telefono_Emergencia { get; set; }

        public int ID { get; set; }

        public Customer(string Nombres, string Apellidos, string Rut, string Numero_Pasaporte, string Direccion, int Numero_Direccion, int Numero_Telefono, string Nombres_Emergencia, string Apellidos_Emergencia, int Numero_Telefono_Emergencia, int ID)
        {
            this.Nombres = Nombres;
            this.Apellidos = Apellidos;
            this.Rut = Rut;
            this.Numero_Pasaporte = Numero_Pasaporte;
            this.Direccion = Direccion;
            this.Numero_Direccion = Numero_Direccion;
            this.Numero_Telefono = Numero_Telefono;
            this.Nombres_Emergencia = Nombres_Emergencia;
            this.Apellidos_Emergencia = Apellidos_Emergencia;
            this.Numero_Telefono_Emergencia = Numero_Telefono_Emergencia;
            this.ID = ID;
        }
    }
}