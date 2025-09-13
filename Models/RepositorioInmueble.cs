using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositorioInmueble : RepositorioBase
{   
    public RepositorioInmueble() { }

    public void Alta(Inmueble inmueble)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO inmueble 
                            (direccion, uso, tipo, cantidadAmb, coordenadas, precio, idPropietario, estado)
                            VALUES (@direccion, @uso, @tipo, @cantidadAmb, @coordenadas, @precio, @idPropietario, @estado);";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.direccion);  
                command.Parameters.AddWithValue("@uso", inmueble.uso);
                command.Parameters.AddWithValue("@tipo", inmueble.tipo);
                command.Parameters.AddWithValue("@cantidadAmb", inmueble.cantidadAmb);
                command.Parameters.AddWithValue("@coordenadas", inmueble.coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.precio);
                command.Parameters.AddWithValue("@idPropietario", inmueble.idPropietario);
                command.Parameters.AddWithValue("@estado", inmueble.estado);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Modificacion(Inmueble inmueble)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE inmueble 
                            SET direccion=@direccion, uso=@uso, tipo=@tipo, cantidadAmb=@cantidadAmb, coordenadas=@coordenadas, precio=@precio, idPropietario=@idPropietario, estado=@estado
                            WHERE idInmueble = @idInmueble";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.direccion); 
                command.Parameters.AddWithValue("@uso", inmueble.uso);
                command.Parameters.AddWithValue("@tipo", inmueble.tipo);
                command.Parameters.AddWithValue("@cantidadAmb", inmueble.cantidadAmb);
                command.Parameters.AddWithValue("@coordenadas", inmueble.coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.precio);
                command.Parameters.AddWithValue("@idPropietario", inmueble.idPropietario);
                command.Parameters.AddWithValue("@estado", inmueble.estado);
                command.Parameters.AddWithValue("@idInmueble", inmueble.idInmueble);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }

    public void Baja(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM inmueble WHERE idInmueble = @idInmueble";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idInmueble", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public List<Inmueble> ObtenerTodos()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM inmueble";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble();
                        inmueble.idInmueble = reader.GetInt32("idInmueble");
                        inmueble.direccion = reader.GetString("direccion");
                        inmueble.uso = reader.GetString("uso");
                        inmueble.tipo = reader.GetString("tipo");
                        inmueble.cantidadAmb = reader.GetInt32("cantidadAmb");
                        inmueble.coordenadas = reader.GetString("coordenadas");
                        inmueble.precio = reader.GetDecimal("precio");
                        inmueble.idPropietario = reader.GetInt32("idPropietario");
                        inmueble.estado = reader.GetBoolean("estado");
                        inmuebles.Add(inmueble);
                    }
                }
            }
            connection.Close();
        }
        return inmuebles;
    }

}