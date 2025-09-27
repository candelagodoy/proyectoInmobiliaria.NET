namespace proyectoInmobiliaria.NET.Models;

using MySql.Data.MySqlClient;


public class RepositorioTipoInmueble : RepositorioBase
{

    public int Alta(TipoInmueble tipoInmueble)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO TiposInmuebles 
                            (nombre, descripcion)
                            VALUES (@nombre, @descripcion);";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nombre", tipoInmueble.nombre);
                command.Parameters.AddWithValue("@descripcion", tipoInmueble.descripcion);
                connection.Open();
                command.ExecuteNonQuery();
                res = (int)command.LastInsertedId; // ID del nuevo Propietario
            }
        }
        return res;
    }

    public void Baja(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM TiposInmuebles WHERE idTipoInmueble = @idTipoInmueble";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idTipoInmueble", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Modificacion(TipoInmueble tipoInmueble)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE TiposInmuebles 
                            SET nombre=@nombre, descripcion=@descripcion
                            WHERE idTipoInmueble = @idTipoInmueble;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idTipoInmueble", tipoInmueble.idTipoInmueble);
                command.Parameters.AddWithValue("@nombre", tipoInmueble.nombre);
                command.Parameters.AddWithValue("@descripcion", tipoInmueble.descripcion);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public List<TipoInmueble> ObtenerTodos()
    {
        List<TipoInmueble> tiposInmuebles = new List<TipoInmueble>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM TiposInmuebles";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoInmueble tipoInmueble = new TipoInmueble();
                        tipoInmueble.idTipoInmueble = reader.GetInt32("idTipoInmueble");
                        tipoInmueble.nombre = reader.GetString("nombre");
                        tipoInmueble.descripcion = reader.GetString("descripcion");
                        tiposInmuebles.Add(tipoInmueble);
                    }
                }
            }
        }
        return tiposInmuebles;
    }

    public TipoInmueble ObtenerPorId(int id)
    {
        TipoInmueble tipoInmueble = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM TiposInmuebles WHERE idTipoInmueble = @idTipoInmueble";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idTipoInmueble", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoInmueble = new TipoInmueble();
                        tipoInmueble.idTipoInmueble = reader.GetInt32("idTipoInmueble");
                        tipoInmueble.nombre = reader.GetString("nombre");
                        tipoInmueble.descripcion = reader.GetString("descripcion");
                    }
                }
            }
        }
        return tipoInmueble;
    }   
    

}


