using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;
public class Propietario
{
    [Display(Name = "Codigo ID")]

    public int id { get; set; }

    [Display(Name = "Nombre")]

    public string? nombre { get; set; }

    [Display(Name = "Apellido")]

    public string? apellido { get; set; }

    [Display(Name = "DNI")]

    public string? dni { get; set; } 

    [Display(Name = "Direccion")]

    public string? direccion { get; set; } 

    [Display(Name = "Celular")]

    public string? celular { get; set; } 

    [Display(Name = "Estado")]
    
    public Boolean estado { get; set; }

    public override string ToString()
    {
        var res = $"{nombre}" + $", {apellido}";
        if (!String.IsNullOrEmpty(dni))
        {
            res += $", {dni}";
        }
        return res;
    }
}