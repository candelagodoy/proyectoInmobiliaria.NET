using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;

public class Inquilino
{
    [Display(Name = "Id")]
    public int id { get; set; }
    [Display(Name = "Nombre")]
    public string? nombre { get; set; }
    [Display(Name = "Apellido")]
    public string? apellido { get; set; }
    [Display(Name = "DNI")]
    public string? dni { get; set; }
    [Display(Name = "Email")]
    public string? email { get; set; }
    [Display(Name = "Celular")]
    public string? celular { get; set; }
    public Boolean estado { get; set; }
    
    override
    public String ToString() => $"{apellido}, {nombre}";
}