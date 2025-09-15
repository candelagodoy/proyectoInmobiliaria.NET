using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositorioContrato : RepositorioBase
{
    public List<Contrato> ObtenerTodos()
    {
        List<Contrato> contratos = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM contrato";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato();
                        contrato.idContrato = reader.GetInt32("idContrato");
                        contrato.fechaDesde = reader.GetDateTime("fechaDesde");
                        contrato.fechaHasta = reader.GetDateTime("fechaHasta");
                        contrato.monto = reader.GetDecimal("monto");
                        contrato.idInmueble = reader.GetInt32("idInmueble");
                        contrato.idInquilino = reader.GetInt32("idInquilino");
                        contrato.idUsuario = reader.GetInt32("idUsuario");
                        contrato.estado = reader.GetBoolean("estado");
                        contratos.Add(contrato);
                    }
                }
            }
        }
        return contratos;
    }

    public Contrato? ObtenerPorId(int id)
    {
        Contrato? contrato = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM contrato WHERE idContrato = @idContrato";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idContrato", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato = new Contrato();
                        contrato.idContrato = reader.GetInt32("idContrato");
                        contrato.fechaDesde = reader.GetDateTime("fechaDesde");
                        contrato.fechaHasta = reader.GetDateTime("fechaHasta");
                        contrato.monto = reader.GetDecimal("monto");
                        contrato.idInmueble = reader.GetInt32("idInmueble");
                        contrato.idInquilino = reader.GetInt32("idInquilino");
                        contrato.idUsuario = reader.GetInt32("idUsuario");
                        contrato.estado = reader.GetBoolean("estado");
                    }
                }
            }
        }
        return contrato;
    }

    public void Baja(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM contrato WHERE idContrato = @idContrato";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idContrato", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Alta(Contrato contrato)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO contrato (fechaDesde, fechaHasta, monto, idInmueble, idInquilino, idUsuario, estado)
                            VALUES (@fechaDesde, @fechaHasta, @monto, @idInmueble, @idInquilino, @idUsuario, @estado);";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@fechaDesde", contrato.fechaDesde);
                command.Parameters.AddWithValue("@fechaHasta", contrato.fechaHasta);
                command.Parameters.AddWithValue("@monto", contrato.monto);
                command.Parameters.AddWithValue("@idInmueble", contrato.idInmueble);
                command.Parameters.AddWithValue("@idInquilino", contrato.idInquilino);
                command.Parameters.AddWithValue("@idUsuario", contrato.idUsuario);
                command.Parameters.AddWithValue("@estado", contrato.estado);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Modificacion(Contrato contrato)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE contrato SET 
                            fechaDesde = @fechaDesde,
                            fechaHasta = @fechaHasta,
                            monto = @monto,
                            idInmueble = @idInmueble,
                            idInquilino = @idInquilino,
                            idUsuario = @idUsuario,
                            estado = @estado
                            WHERE idContrato = @idContrato;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idContrato", contrato.idContrato);
                command.Parameters.AddWithValue("@fechaDesde", contrato.fechaDesde);
                command.Parameters.AddWithValue("@fechaHasta", contrato.fechaHasta);
                command.Parameters.AddWithValue("@monto", contrato.monto);
                command.Parameters.AddWithValue("@idInmueble", contrato.idInmueble);
                command.Parameters.AddWithValue("@idInquilino", contrato.idInquilino);
                command.Parameters.AddWithValue("@idUsuario", contrato.idUsuario);
                command.Parameters.AddWithValue("@estado", contrato.estado);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }   
    
}
    