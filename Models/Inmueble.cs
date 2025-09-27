using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliaria.NET.Models;

 public enum UsoInmueble
    {
        Comercial = 1,
        Residencial = 2
    }

    /*public enum TipoInmueble
    {
        Local = 1,
        Deposito = 2,
        Casa = 3,
        Departamento = 4
    }*/

public class Inmueble
{


    [Display(Name = "Id")]

    public int idInmueble { get; set; }

    [Display(Name = "Direccion")]

    public string? direccion { get; set; }

    [Display(Name = "Uso")]

    public UsoInmueble uso { get; set; }

    [Display(Name = "Tipo")]

    public TipoInmueble tipo { get; set; }

    [Display(Name = "Cantidad Ambientes")]

    public int cantidadAmb { get; set; }

    [Display(Name = "Coordenadas")]

    public string? coordenadas { get; set; }

    [Display(Name = "Precio")]

    public decimal precio { get; set; }

    [Display(Name = "Propietario")]

    public int idPropietario { get; set; }

    [Display(Name = "Estado")]

    public bool estado { get; set; }

    override
    public String ToString() => $"{direccion}";
}