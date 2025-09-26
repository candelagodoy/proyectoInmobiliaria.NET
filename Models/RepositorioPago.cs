using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models
{
    public class RepositorioPago : RepositorioBase
    {
        public void Alta(Pago pago)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO pago VALUES (@idPago, @descripcion, @idContrato, @fechaPago, @importe, @estado, @numPago, @idUsuarioAlta, NULL);";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", pago.idPago);
                    command.Parameters.AddWithValue("@descripcion", pago.descripcion);
                    command.Parameters.AddWithValue("@idContrato", pago.idContrato);
                    command.Parameters.AddWithValue("@fechaPago", pago.fechaPago);
                    command.Parameters.AddWithValue("@importe", pago.importe);
                    command.Parameters.AddWithValue("@estado", pago.estado);
                    List<Pago> pagos = ObtenerPorContrato(pago.idContrato);
                    if (pagos.Count == 0)
                    {
                        pago.numPago = 1;
                    }
                    else
                    {
                        pago.numPago = pagos[pagos.Count - 1].numPago + 1;
                    }
                    command.Parameters.AddWithValue("@numPago", pago.numPago);
                    command.Parameters.AddWithValue("@idUsuarioAlta", pago.idUsuarioAlta);
                    command.Parameters.AddWithValue("@idUsuarioBaja", pago.idUsuarioBaja);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Baja(int idPago, int idUsuarioLogeado)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE pago SET estado = 0, idUsuarioBaja = @idUsuarioBaja WHERE idPago = @idPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", idPago);
                    command.Parameters.AddWithValue("@idUsuarioBaja", idUsuarioLogeado);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void ModificacionDescripcion(Pago pago)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE pago SET descripcion=@descripcion WHERE idPago = @idPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", pago.idPago);
                    command.Parameters.AddWithValue("@descripcion", pago.descripcion);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<Pago> ObtenerTodos()
        {
            List<Pago> pagos = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT * FROM pago WHERE estado = 1;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pago pago = new Pago();
                            pago.idPago = reader.GetInt32("idPago");
                            pago.descripcion = reader.GetString("descripcion");
                            pago.idContrato = reader.GetInt32("idContrato");
                            pago.fechaPago = reader.GetDateTime("fechaPago");
                            pago.importe = reader.GetDecimal("importe");
                            pago.estado = reader.GetBoolean("estado");
                            pago.numPago = reader.GetInt32("numPago");
                            pago.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                            var ordIdUsuarioBaja = reader.GetOrdinal("idUsuarioBaja");
                            pago.idUsuarioBaja = reader.IsDBNull(ordIdUsuarioBaja)
                          ? (int?)null
                          : reader.GetInt32(ordIdUsuarioBaja);
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

        public Pago ObtenerPorId(int id)
        {
            Pago pago = new Pago();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT * FROM pago WHERE idPago = @idPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pago.idPago = reader.GetInt32("idPago");
                            pago.descripcion = reader.GetString("descripcion");
                            pago.idContrato = reader.GetInt32("idContrato");
                            pago.fechaPago = reader.GetDateTime("fechaPago");
                            pago.importe = reader.GetDecimal("importe");
                            pago.estado = reader.GetBoolean("estado");
                            pago.numPago = reader.GetInt32("numPago");
                            pago.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                            var ordIdUsuarioBaja = reader.GetOrdinal("idUsuarioBaja");
                            pago.idUsuarioBaja = reader.IsDBNull(ordIdUsuarioBaja)
                          ? (int?)null
                          : reader.GetInt32(ordIdUsuarioBaja);
                        }
                    }
                }
                connection.Close();
            }
            return pago;
        }

        public List<Pago> ObtenerPorContrato(int idContrato)
        {
            List<Pago> pagos = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT * FROM pago WHERE idContrato = @idContrato AND estado = 1;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContrato", idContrato);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pago pago = new Pago();
                            pago.idPago = reader.GetInt32("idPago");
                            pago.descripcion = reader.GetString("descripcion");
                            pago.idContrato = reader.GetInt32("idContrato");
                            pago.fechaPago = reader.GetDateTime("fechaPago");
                            pago.importe = reader.GetDecimal("importe");
                            pago.estado = reader.GetBoolean("estado");
                            pago.numPago = reader.GetInt32("numPago");
                            pago.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                            var ordIdUsuarioBaja = reader.GetOrdinal("idUsuarioBaja");
                            pago.idUsuarioBaja = reader.IsDBNull(ordIdUsuarioBaja)
                          ? (int?)null
                          : reader.GetInt32(ordIdUsuarioBaja);
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

        public List<Pago> PagosAnulados()
        {
            List<Pago> pagos = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT * FROM pago WHERE estado = 0;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pago pago = new Pago();
                            pago.idPago = reader.GetInt32("idPago");
                            pago.descripcion = reader.GetString("descripcion");
                            pago.idContrato = reader.GetInt32("idContrato");
                            pago.fechaPago = reader.GetDateTime("fechaPago");
                            pago.importe = reader.GetDecimal("importe");
                            pago.estado = reader.GetBoolean("estado");
                            pago.numPago = reader.GetInt32("numPago");
                            pago.idUsuarioAlta = reader.GetInt32("idUsuarioAlta");
                            pago.idUsuarioBaja = reader.IsDBNull(reader.GetOrdinal("idUsuarioBaja"))
                     ? (int?)null
                     : reader.GetInt32("idUsuarioBaja");
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

    }
}