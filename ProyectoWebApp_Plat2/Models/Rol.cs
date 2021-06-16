using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebApp_Plat2.Models
{
    
    public class Rol
    {
        public string Email { get; set; }

        public string Contrasenia { get; set; }

        public bool ID_Rol { get; set; }

        public int ID { get; set; }

        public Rol(string Email, string Contrasenia, bool ID_Rol, int ID)
        {
            this.Email = Email;
            this.Contrasenia = Contrasenia;
            this.ID_Rol = ID_Rol;
            this.ID = ID;
        }
    }
}