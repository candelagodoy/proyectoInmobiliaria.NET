
using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositorioUsuario : RepositorioBase
{
 public int Alta(Usuario usuario)
{
    int id = 0;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string sql = @"INSERT INTO usuario 
                        (nombre, apellido, email, clave, avatar, rol)
                        VALUES (@nombre, @apellido, @email, @clave, @avatar, @rol);
                       SELECT LAST_INSERT_ID();";  // devuelve el id recién insertado

        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nombre", usuario.nombre);
            command.Parameters.AddWithValue("@apellido", usuario.apellido);
            command.Parameters.AddWithValue("@email", usuario.email);
            command.Parameters.AddWithValue("@clave", usuario.clave);
            command.Parameters.AddWithValue("@avatar", usuario.avatar);
            command.Parameters.AddWithValue("@rol", usuario.rol);

            connection.Open();
            id = Convert.ToInt32(command.ExecuteScalar()); // obtiene el id
            connection.Close();
        }
    }
    return id;
}


    public void Baja(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM usuario WHERE idUsuario = @idUsuario";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Modificacion(Usuario usuario)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE usuario
                            SET nombre=@nombre, apellido=@apellido, email=@email, clave=@clave, avatar=@avatar, rol=@rol
                            WHERE idUsuario = @idUsuario;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nombre", usuario.nombre);
                command.Parameters.AddWithValue("@apellido", usuario.apellido);
                command.Parameters.AddWithValue("@email", usuario.email);
                command.Parameters.AddWithValue("@clave", usuario.clave);
                command.Parameters.AddWithValue("@avatar", usuario.avatar);
                command.Parameters.AddWithValue("@rol", usuario.rol);
                command.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public List<Usuario> ObtenerTodos()
    {
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM usuario";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.idUsuario = reader.GetInt32("idUsuario");
                        usuario.nombre = reader.GetString("nombre");
                        usuario.apellido = reader.GetString("apellido");
                        usuario.email = reader.GetString("email");
                        usuario.clave = reader.GetString("clave");
                        usuario.avatar = reader.GetString("avatar");
                        usuario.rol = reader.GetInt16("rol");
                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }

    public Usuario? ObtenerPorId(int id)
    {
        Usuario? usuario = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM usuario WHERE idUsuario = @idUsuario";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.idUsuario = reader.GetInt32("idUsuario");
                        usuario.nombre = reader.GetString("nombre");
                        usuario.apellido = reader.GetString("apellido");
                        usuario.email = reader.GetString("email");
                        usuario.clave = reader.GetString("clave");
                        usuario.avatar = reader.GetString("avatar");
                        usuario.rol = reader.GetInt16("rol");
                    }
                }
            }
        }
        return usuario;
    }

    public Usuario? ObtenerPorEmail(string email)
    {
        Usuario? usuario = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var sql = "SELECT * FROM usuario WHERE email = @email";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.idUsuario = reader.GetInt32("idUsuario");
                        usuario.nombre = reader.GetString("nombre");
                        usuario.apellido = reader.GetString("apellido");
                        usuario.email = reader.GetString("email");
                        usuario.clave = reader.GetString("clave");
                        usuario.avatar = reader.GetString("avatar");
                        usuario.rol = reader.GetInt16("rol");
                    }
                }
            }
        }
        return usuario;
    }
    
}
    