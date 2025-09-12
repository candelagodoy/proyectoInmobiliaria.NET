using MySql.Data.MySqlClient;

namespace proyectoInmobiliaria.NET.Models;

public class RepositoprioContrato : RepositorioBase
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
}
    