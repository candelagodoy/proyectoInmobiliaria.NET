using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositorioInquilino : RepositorioBase
{
    public void Baja(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM inquilinos WHERE idInquilino = @idInquilino";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idInquilino", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }


    public void Alta(Inquilino inquilino)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO inquilinos 
                            (dni, apellido, nombre, celular, email, estado)
                            VALUES (@dni, @apellido, @nombre, @celular,@email, @estado);";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@dni", inquilino.dni);
                command.Parameters.AddWithValue("@apellido", inquilino.apellido);
                command.Parameters.AddWithValue("@nombre", inquilino.nombre);
                command.Parameters.AddWithValue("@celular", inquilino.celular);
                command.Parameters.AddWithValue("@email", inquilino.email);
                command.Parameters.AddWithValue("@estado", inquilino.estado);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

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
                        inquilino.id = reader.GetInt32("idInquilino");
                        inquilino.dni = reader.GetString("dni");
                        inquilino.apellido = reader.GetString("apellido");
                        inquilino.nombre = reader.GetString("nombre");
                        inquilino.celular = reader.GetString("celular");
                        inquilino.email = reader.GetString("email");
                        inquilino.estado = reader.GetBoolean("estado");
                        inquilinos.Add(inquilino);
                    }
                }
            }
            connection.Close();
        }
        return inquilinos;
    }

    public Inquilino? ObtenerPorId(int id)
    {
        Inquilino? inquilino = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM inquilinos WHERE idInquilino = @idInquilino";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idInquilino", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inquilino = new Inquilino();
                        inquilino.id = reader.GetInt32("idInquilino");
                        inquilino.dni = reader.GetString("dni");
                        inquilino.apellido = reader.GetString("apellido");
                        inquilino.nombre = reader.GetString("nombre");
                        inquilino.celular = reader.GetString("celular");
                        inquilino.email = reader.GetString("email");
                        inquilino.estado = reader.GetBoolean("estado");
                    }
                }
            }
            connection.Close();
        }
        return inquilino;
    }  
    public void Modificacion(Inquilino inquilino)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE inquilinos 
                            SET dni=@dni, apellido=@apellido, nombre=@nombre, celular=@celular, email=@email, estado=@estado
                            WHERE idInquilino = @idInquilino";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@dni", inquilino.dni);
                command.Parameters.AddWithValue("@apellido", inquilino.apellido);
                command.Parameters.AddWithValue("@nombre", inquilino.nombre);
                command.Parameters.AddWithValue("@celular", inquilino.celular);
                command.Parameters.AddWithValue("@email", inquilino.email);
                command.Parameters.AddWithValue("@estado", inquilino.estado);
                command.Parameters.AddWithValue("@idInquilino", inquilino.id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}

