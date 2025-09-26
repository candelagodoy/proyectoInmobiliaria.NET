using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoInmobiliaria.NET.Models
{
    public class Pago
    {
        [Key]
        [Display(Name = "Código")]
        public int idPago { get; set; }

        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        [Display(Name = "Nº de Contrato")]
        [ForeignKey("idContrato")]
        public int idContrato { get; set; }

        [Required]
        [Display(Name = "Fecha de Pago")]
        public DateTime fechaPago { get; set; }
        [Required]
        [Display(Name = "Importe($)")]
        public decimal importe { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }

        [Display(Name = "Nº de Pago")]
        public int numPago { get; set; }

        [Display(Name = "Nº de Usuario ALTA")]
        public int idUsuarioAlta { get; set; } 

        [Display(Name = "Nº de Usuario BAJA")]
        public int? idUsuarioBaja { get; set; }
    }
}