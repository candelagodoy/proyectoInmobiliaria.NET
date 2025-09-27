using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models
{


    public class TipoInmueble
    {
        [Key]
        [Display(Name = "Id Tipo Inmueble")]
        public int idTipoInmueble { get; set; }

        [Display(Name = "Nombre Tipo Inmueble")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        
        public String ToString() => $"{idTipoInmueble}, {nombre}, {descripcion}";
    }
}

