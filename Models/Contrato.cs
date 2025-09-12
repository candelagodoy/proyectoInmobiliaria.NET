using System.ComponentModel.DataAnnotations;
    
namespace proyectoInmobiliaria.NET.Models;

public class Contrato
{
    public int idContrato { get; set; }
    public DateTime fechaDesde { get; set; }
    public DateTime fechaHasta { get; set; }
    public int idInquilino { get; set; }
    public int idInmueble { get; set; }
    public decimal monto { get; set; }
    public int idUsuario { get; set; }
    public bool estado { get; set; }


}