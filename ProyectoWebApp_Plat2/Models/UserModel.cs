using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace ProyectoWebApp_Plat2.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    public class UserModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string contrasenia { get; set; }
    }
}