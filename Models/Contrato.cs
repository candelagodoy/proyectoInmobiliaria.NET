using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;

public class Contrato
{
    [Display(Name = "Id")]

    public int idContrato { get; set; }

    [Display(Name = "Fecha desde")]
      [Required(ErrorMessage = "La fecha desde es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime fechaDesde { get; set; }

    [Display(Name = "Fecha hasta")]
    [Required(ErrorMessage = "La fecha hasta es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime fechaHasta { get; set; }

    [Display(Name = "Inquilino")]
    [Required(ErrorMessage = "El inquilino es obligatorio")]
    public int idInquilino { get; set; }

    [Display(Name = "Inmueble")]

    [Required(ErrorMessage = "El inmueble es obligatorio")]
    public int idInmueble { get; set; }

    [Display(Name = "Monto")]
    [Required(ErrorMessage = "El monto es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal monto { get; set; }

    [Display(Name = "Usuario alta")]

    public int idUsuarioAlta { get; set; }

    [Display(Name = "Usuario baja")]
    public int? idUsuarioBaja { get; set; }

    [Display(Name = "Estado")]

    public bool estado { get; set; }

    override
        public String ToString() => $"{idContrato}, {fechaDesde}, {fechaHasta}, {monto}, {idInmueble}, {idInquilino}, {idUsuarioAlta}, {idUsuarioBaja}, {estado}";
}