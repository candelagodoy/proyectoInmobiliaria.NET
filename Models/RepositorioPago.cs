using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models
{
    public class RepositorioPago : RepositorioBase
    {
        public void Alta(Pago pago)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO pago VALUES (@idPago, @descripcion, @idContrato, @fechaPago, @importe, @estado, @numPago, @idUsuario);";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", pago.idPago);
                    command.Parameters.AddWithValue("@descripcion", pago.descripcion);
                    command.Parameters.AddWithValue("@idContrato", pago.idContrato);
                    command.Parameters.AddWithValue("@fechaPago", pago.fechaPago);
                    command.Parameters.AddWithValue("@importe", pago.importe);
                    command.Parameters.AddWithValue("@estado", pago.estado);
                    command.Parameters.AddWithValue("@numPago", pago.numPago);
                    command.Parameters.AddWithValue("@idUsuario", pago.idUsuario);
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
                string sql = "UPDATE pago SET estado = 0 WHERE idPago = @idPago;";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPago", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
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
                var sql = "SELECT * FROM pago;";
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
                            pagos.Add(pago);
                        }
                    }
                }
                connection.Close();
            }
            return pagos;
        }

        public List<Pago> ObtenerPorContrato(int idContrato)
        {
            List<Pago> pagos = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sql = "SELECT * FROM pago WHERE idContrato = @idContrato;";
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