using Microsoft.Data.SqlClient;
using Productos.API.Modal.BaseDatos.Core;
using Productos.API.Modal.Entidades;
using System.Data;

namespace Productos.API.Modal.BaseDatos.DAO
{
    public class SecurityDAO : ISecurityDAO
    {
        private readonly string _connectionString;
        public SecurityDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontro la cadena de conexion.");
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            bool resultado = false;
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.Login, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", loginRequest.username);
                cmd.Parameters.AddWithValue("@password", loginRequest.password);

                await conn.OpenAsync();
                var existe = await cmd.ExecuteScalarAsync();

                if (existe != null && existe != DBNull.Value)
                {
                    resultado = Convert.ToBoolean(existe);
                }
            }
            catch (SqlException se)
            {
                if (se.Number == 50000)
                {
                    throw new InvalidOperationException($"Se genero un error en Base de Datos: {se.Message}");
                }
                else
                {
                    throw new Exception(se.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado", ex);
            }
            return resultado;
        }
    }
}
