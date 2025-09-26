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
            string sql = "SELECT * FROM contrato WHERE estado = 1;";
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
                        contrato.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                        var ordIdUsuarioBaja = reader.GetOrdinal("idUsuarioBaja");
                        contrato.idUsuarioBaja = reader.IsDBNull(ordIdUsuarioBaja)
                            ? (int?)null
                            : reader.GetInt32(ordIdUsuarioBaja);
                        contrato.estado = reader.GetBoolean("estado");
                        contratos.Add(contrato);
                    }
                }
            }
        }
        return contratos;
    }

    public List<Contrato> ObtenerContratosTerminados()
    {
        List<Contrato> contratos = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM contrato WHERE estado = 0;";
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
                        contrato.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                        contrato.idUsuarioBaja = reader.IsDBNull(reader.GetOrdinal("idUsuarioBaja"))
    ? null
    : reader.GetInt32("idUsuarioBaja");
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
            string sql = "SELECT * FROM contrato WHERE idContrato = @idContrato AND estado = 1";
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
                        contrato.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                        var ordIdUsuarioBaja = reader.GetOrdinal("idUsuarioBaja");
                        contrato.idUsuarioBaja = reader.IsDBNull(ordIdUsuarioBaja)
                      ? (int?)null
                      : reader.GetInt32(ordIdUsuarioBaja);
                        contrato.estado = reader.GetBoolean("estado");
                    }
                }
            }
        }
        return contrato;
    }

    public void Baja(int id, int idUsuarioLogeado)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string sql = "UPDATE contrato SET estado = 0 , idUsuarioBaja = @idUsuarioBaja WHERE idContrato = @idContrato;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idContrato", id);
                command.Parameters.AddWithValue("@idUsuarioBaja", idUsuarioLogeado);
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
            string sql = @"INSERT INTO contrato (fechaDesde, fechaHasta, monto, idInmueble, idInquilino, idUsuarioAlta, idUsuarioBaja, estado)
                            VALUES (@fechaDesde, @fechaHasta, @monto, @idInmueble, @idInquilino, @idUsuarioAlta, NULL , @estado);";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@fechaDesde", contrato.fechaDesde);
                command.Parameters.AddWithValue("@fechaHasta", contrato.fechaHasta);
                command.Parameters.AddWithValue("@monto", contrato.monto);
                command.Parameters.AddWithValue("@idInmueble", contrato.idInmueble);
                command.Parameters.AddWithValue("@idInquilino", contrato.idInquilino);
                command.Parameters.AddWithValue("@idUsuarioAlta", contrato.idUsuarioAlta);
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
                            idUsuarioAlta = @idUsuarioAlta,
                            idUsuarioBaja = @idUsuarioBaja,
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
                command.Parameters.AddWithValue("@idUsuarioAlta", contrato.idUsuarioAlta);
                command.Parameters.AddWithValue("@idUsuarioBaja", contrato.idUsuarioBaja);
                command.Parameters.AddWithValue("@estado", contrato.estado);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }


}
