namespace proyectoInmobiliaria.NET.Models;

using MySql.Data.MySqlClient;

public class RepositorioPropietario : RepositorioBase
{

    public int Alta(Propietario p)
{
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string sql = @"INSERT INTO Propietarios 
                        (nombre, apellido, dni, direccion, celular, estado)
                        VALUES (@nombre, @apellido, @dni, @direccion, @celular, @estado);";

        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nombre", p.nombre);
            command.Parameters.AddWithValue("@apellido", p.apellido);
            command.Parameters.AddWithValue("@dni", p.dni);
            command.Parameters.AddWithValue("@direccion", p.direccion);
            command.Parameters.AddWithValue("@celular", p.celular);
            command.Parameters.AddWithValue("@estado", p.estado);

            connection.Open();
            command.ExecuteNonQuery();
            res = (int)command.LastInsertedId; // ID del nuevo Propietario
        }
    }
    return res;
}

    
        public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "DELETE FROM Propietarios WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

        public int Modificacion(Propietario p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Propietarios 
					SET nombre=@nombre, apellido=@apellido, dni=@dni, celular=@celular, direccion=@direccion, estado=@estado
					WHERE id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@nombre", p.nombre);
					command.Parameters.AddWithValue("@apellido", p.apellido);
					command.Parameters.AddWithValue("@dni", p.dni);
					command.Parameters.AddWithValue("@celular", p.celular);
					command.Parameters.AddWithValue("@direccion", p.direccion);
					command.Parameters.AddWithValue("@estado", p.estado);
					command.Parameters.AddWithValue("@id", p.id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

    public List<Propietario> ObtenerTodos()
    {
        List<Propietario> propietarios = new List<Propietario>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                        propietario.dni = reader.GetString("dni");
                        propietario.direccion = reader.GetString("direccion");
                        propietario.celular = reader.GetString("celular");
                        propietario.estado = reader.GetBoolean("estado");

                        propietarios.Add(propietario);
                    }
                }
            }
        }
        return propietarios;
    }
}