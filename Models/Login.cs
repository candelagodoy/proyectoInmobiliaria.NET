
using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models
{
    public class Login
    {

         [Required, EmailAddress]
        public string Email { get; set; }
         [Required, DataType(DataType.Password)]

        public string Clave { get; set; }
    }
}