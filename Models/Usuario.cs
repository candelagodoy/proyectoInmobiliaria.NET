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
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public string avatar { get; set; } = "";
        public int rol { get; set; }
        public string rolNombre => rol > 0 ? ((enRoles)rol).ToString() : "";
        
        override
        public string ToString() => $"{apellido}, {nombre} ({email})";    
	}
}