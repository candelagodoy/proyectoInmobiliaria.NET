namespace proyectoInmobiliaria.NET.Models;
public class Inquilino
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public string? dni { get; set; }
    public string? email { get; set; }
    public string? celular { get; set; }
    public Boolean estado { get; set; }
}