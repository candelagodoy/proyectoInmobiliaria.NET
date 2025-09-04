using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;
public class Propietario
{
    [Display(Name = "Codigo ID")]
    public int id { get; set; }
    [Display(Name = "Nombre Propietario")]
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public string? dni { get; set; } 
    public string? direccion { get; set; } 
    public string? celular { get; set; } 
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