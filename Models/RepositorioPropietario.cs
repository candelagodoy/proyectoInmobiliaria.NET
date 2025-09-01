using MySql.Data.MySqlClient;

public class RespositorioPropietario
{
    string ConectionString = "Server=localhost;User=root;Password=;Database=test;SslMode=none";

    public List<Propietario> obtenerPropietarios()
    {
        List<Propietario> propietarios = new List<Propietario>();

        using (MySqlConnection connection = new MySqlConnection(ConectionString))
        {
            var sql = "SELECT * FROM propietarios";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario propietario = new Propietario();
                        propietario.id = reader.GetInt32("id");
                        propietario.nombre = reader.GetString("nombre");
                        propietario.apellido = reader.GetString("apellido");
                        propietario.direccion = reader.GetString("direccion");
                        propietario.celular = reader.GetString("celular");

                        propietarios.Add(propietario);
                    }
                }
            }
        }        
        return propietarios;
    }
}