using Microsoft.Data.SqlClient;
using Productos.API.Modal.BaseDatos.Core;
using Productos.API.Modal.Entidades;
using System.Data;

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
                using var cmd = new SqlCommand(SP.SQL.ObtieneProductos, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Entidades.Productos
                    {
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                        CostoReventa = reader.GetDecimal(reader.GetOrdinal("CostoReventa")),
                        IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                        NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
                        EmpresaProveedor = reader.GetString(reader.GetOrdinal("EmpresaProveedor")),
                        IdTipoProducto = reader.GetInt32(reader.GetOrdinal("IdTipoProducto")),
                        NombreTipoProducto = reader.GetString(reader.GetOrdinal("NombreTipoProducto")),
                        PrecioProducto = reader.GetDecimal(reader.GetOrdinal("PrecioProducto")),
                        IdProductoProveedor = reader.GetString(reader.GetOrdinal("IdProductoProveedor"))
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado", ex);
            }
        }

        public async Task<List<Proveedores>> ObtenerProveedoresAsync()
        {
            try
            {
                var lista = new List<Entidades.Proveedores>();

                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SP.SQL.ObtieneProveedores, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Entidades.Proveedores
                    {
                        IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                        NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
                        EmpresaProveedor  = reader.GetString(reader.GetOrdinal("EmpresaProveedor")),
                        DescripcionProveedor = reader.GetString(reader.GetOrdinal("DescripcionProveedor"))
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado", ex);
            }
        }

        public async Task<List<TiposProductos>> ObtenerTiposAsync()
        {
            try
            {
                var lista = new List<Entidades.TiposProductos>();

                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(SP.SQL.ObtieneTipos, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Entidades.TiposProductos
                    {
                        IdTipoProducto = reader.GetInt32(reader.GetOrdinal("IdTipoProducto")),
                        NombreTipoProducto = reader.GetString(reader.GetOrdinal("NombreTipoProducto")),
                        DescripcionTipo = reader.GetString(reader.GetOrdinal("DescripcionTipo"))
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
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.InsertaProducto, conn);             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@CostoReventa", producto.CostoReventa);
                cmd.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                cmd.Parameters.AddWithValue("@IdTipoProducto", producto.IdTipoProducto);
                cmd.Parameters.AddWithValue("@Precio", producto.PrecioProducto);
                cmd.Parameters.AddWithValue("@IdProductoProveedor", producto.IdProductoProveedor);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();             
            }
            catch (SqlException se)
            {
                if (se.Number == 2627 || se.Number == 2601)
                {
                    throw new InvalidOperationException($"El codigo id de producto del proveedor ya existe en el sistema.");
                }
                else if (se.Number == 50000)
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
            return true;
        }

        public async Task<bool> ActualizaProductoAsync(Entidades.Productos producto)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.ActualizaProducto, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@CostoReventa", producto.CostoReventa);
                cmd.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                cmd.Parameters.AddWithValue("@IdTipoProducto", producto.IdTipoProducto);
                cmd.Parameters.AddWithValue("@Precio", producto.PrecioProducto);
                cmd.Parameters.AddWithValue("@IdProductoProveedor", producto.IdProductoProveedor);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                
            }
            catch (SqlException se)
            {
                if (se.Number == 2627 || se.Number == 2601)
                {
                    throw new InvalidOperationException($"El codigo id de cliente ya existe en el sistema.");
                }
                else if (se.Number == 50000)
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
            return true;
        }

        public async Task<bool> EliminaProductoAsync(int id)
        {
            var filasAfectadas = 0;
            try
            {
                using var conn = new SqlConnection(_connectionString);

                using var cmd = new SqlCommand(SP.SQL.EliminaProducto, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);


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
    }
}
