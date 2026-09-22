using Microsoft.Data.SqlClient;
using Productos.API.Modal.BaseDatos.Core;
using Productos.API.Modal.Entidades;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Productos.API.Modal.BaseDatos.DAO
{
    public class ProductosDAO : IProductosDAO
    {
        private readonly string _connectionString;

        public ProductosDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontro la cadena de conexion.");
        }

        public async Task<List<Entidades.Productos>> ObtenerProductosAsync()
        {
            try
            {
                var lista = new List<Entidades.Productos>();

                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SP.Query.QueryView, conn);

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Entidades.Productos
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                        idCliente = reader.GetString(reader.GetOrdinal("idCliente"))
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado", ex);
            }
        }

        public async Task<bool> InsertaProductoAsync(Entidades.Productos producto)
        {
            var filasAfectadas = 0;
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.InsertaProducto, conn);             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@IdCliente", producto.idCliente);

                await conn.OpenAsync();
                filasAfectadas = await cmd.ExecuteNonQueryAsync();             
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
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizaProductoAsync(Entidades.Productos producto)
        {
            var filasAfectadas = 0;
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.ActualizaProducto, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@IdCliente", producto.idCliente);

                await conn.OpenAsync();
                filasAfectadas = await cmd.ExecuteNonQueryAsync();

                await conn.OpenAsync();
                filasAfectadas = await cmd.ExecuteNonQueryAsync();
                
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
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminaProductoAsync(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.EliminaProducto, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);


                await conn.OpenAsync();
                var filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
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
        }
    }
}
