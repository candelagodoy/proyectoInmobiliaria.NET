namespace proyectoInmobiliaria.NET.Models;
using MySql.Data.MySqlClient;

public abstract class RepositorioBase
{
    protected readonly string ConectionString;

    protected RepositorioBase()
    {
        ConectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria_net;SslMode=none";
    }

   
}