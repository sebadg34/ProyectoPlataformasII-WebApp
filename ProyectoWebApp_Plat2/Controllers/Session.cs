using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoWebApp_Plat2.Models;

namespace ProyectoWebApp_Plat2.Controllers
{
    /// <summary>
    /// Clase que controla las sesiones al sistema
    /// </summary>
    public class Session
    {
        public static Session instance { get; private set; }

        private User logedUser { get; set; }
        private DateTime logedDate { get; set; }
        private string token { get; set; }

        private Session()
        {

        }

        public static Session GetInstance()
        {
            if(instance == null)
            {
                instance = new Session();
            }

            return instance;
        }
    }
}
