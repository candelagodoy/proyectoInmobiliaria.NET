using System.ComponentModel.DataAnnotations;


namespace proyectoInmobiliaria.NET.Models
{
	public enum enRoles
	{
		Administrador = 1,
		Empleado = 2,
	}

    public class Usuario
    {
        [Display(Name = "Código")]
        public int idUsuario { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        public string apellido { get; set; }
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Display(Name = "Contraseña")]
        public string clave { get; set; }
        [Display(Name = "Avatar")]
        public string? avatar { get; set; } = "";
        public IFormFile? avatarFile { get; set; }
        [Display(Name = "Rol")]
        public int rol { get; set; }
        public string rolNombre => rol > 0 ? ((enRoles)rol).ToString() : "";
        
        override
        public string ToString() => $"{apellido}, {nombre} ({email})";    
	}
}