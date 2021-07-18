using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebApp_Plat2.Models
{
    public class Reserve
    {
        public int ID_Flight { get; set; }

        public int ID_Customer { get; set; }

        public int ID { get; set; }

        public Reserve(int ID_Flight, int ID_Customer)
        {
            this.ID_Flight = ID_Flight;
            this.ID_Customer = ID_Customer;
        }
    }
}