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
					(Nombre, Apellido, Dni, Telefono, Email, Clave)
					VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave);
					SELECT SCOPE_IDENTITY();";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email);
					command.Parameters.AddWithValue("@clave", p.Clave);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					p.IdPropietario = res;
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