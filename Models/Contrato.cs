using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;

public class Contrato
{
    [Display(Name = "Id")]

    public int idContrato { get; set; }

    [Display(Name = "Fecha desde")]

    public DateTime fechaDesde { get; set; }

    [Display(Name = "Fecha hasta")]

    public DateTime fechaHasta { get; set; }

    [Display(Name = "Inquilino")]

    public int idInquilino { get; set; }

    [Display(Name = "Inmueble")]    

    public int idInmueble { get; set; }

    [Display(Name = "Monto")]

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