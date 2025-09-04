using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositorioInquilino : RepositorioBase
{

    public RepositorioInquilino() { }

    public List<Inquilino> ObtenerTodos()
    {
        List<Inquilino> inquilinos = new List<Inquilino>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM inquilinos";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = new Inquilino();
                        inquilino.id = reader.GetInt32("id");
                        inquilino.dni = reader.GetString("dni");
                        inquilino.apellido = reader.GetString("apellido");
                        inquilino.nombre = reader.GetString("nombre");
                        inquilino.celular = reader.GetString("celular");
                        inquilino.estado = reader.GetBoolean("estado");
                        inquilinos.Add(inquilino);
                    }
                }
            }
            connection.Close();
        }
        return inquilinos;
    }
}
