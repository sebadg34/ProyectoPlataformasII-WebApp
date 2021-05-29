using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace ProyectoWebApp_Plat2.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "UserName is required")]
        private string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        private string Password { get; set; }
    }
}