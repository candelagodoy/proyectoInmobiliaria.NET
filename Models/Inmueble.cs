using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;

public class Inmueble 
{
    
    public int idInmueble { get; set; }
    
    public string? direccion { get; set; }   
    
    public string? uso { get; set; } 
    
    public string? tipo { get; set; }
    
    public int cantidadAmb { get; set; }
    
    public string? coordenadas { get; set; }
    
    public decimal precio { get; set; }  
    
    public int idPropietario { get; set; }
    
    public bool estado { get; set; }
}