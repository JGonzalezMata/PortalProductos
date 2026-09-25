using PortalProductos.Modelo;
using PortalProductos.Modelo.Entidades;
using System.Net.Http.Json;
using System.Text.Json;

namespace PortalProductos.Controlador
{
    public class ProductoServices : IProductoServices
    {
        #region Variables
        private static List<Productos> _listaProductos = new();
        private static List<Proveedores> _listaProveedores = new();
        private static List<TiposProductos> _listaTipos = new();
        private readonly HttpClient _client;
        #endregion

        public ProductoServices(HttpClient client)
        {
            _client = client;
        }

        #region Consultas
        public async Task<List<Productos>> ObtieneProductosAsync()
        {
            var response = await _client.GetAsync("api/Productos/GetProducts");
            if (response.IsSuccessStatusCode)
            {
                List<Productos>? listaProductos = await response.Content.ReadFromJsonAsync<List<Productos>>();
                if (listaProductos != null)
                {
                    _listaProductos = listaProductos;
                }
            }
            return await Task.FromResult(_listaProductos);
        }

        public async Task<List<Proveedores>> ObtieneProveedoresAsync()
        {
            var response = await _client.GetAsync("api/Productos/GetSuppliers");
            if (response.IsSuccessStatusCode)
            {
                List<Proveedores>? listaProveedores = await response.Content.ReadFromJsonAsync<List<Proveedores>>();
                if (listaProveedores != null)
                {
                    _listaProveedores = listaProveedores;
                }
            }
            return await Task.FromResult(_listaProveedores);
        }

        public async Task<List<TiposProductos>> ObtieneTiposAsync()
        {
            var response = await _client.GetAsync("api/Productos/GetTypes");
            if (response.IsSuccessStatusCode)
            {
                List<TiposProductos>? listaTipos = await response.Content.ReadFromJsonAsync<List<TiposProductos>>();
                if (listaTipos != null)
                {
                    _listaTipos = listaTipos;
                }
            }
            return await Task.FromResult(_listaTipos);
        }

        public async Task<List<Productos>> ConsultaDinamica(string? nombre, string? idProductoProveedor, int? idProveedor, int? idTipoProducto)
        {
            
            var query = _listaProductos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(q => q.NombreProducto.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(idProductoProveedor))
            {
                query = query.Where(q => q.IdProductoProveedor.Contains(idProductoProveedor, StringComparison.OrdinalIgnoreCase));
            }

            if (idProveedor > 0)
            {
                query = query.Where(q => q.IdProveedor == idProveedor);
            }

            if (idTipoProducto > 0)
            {
                query = query.Where(q => q.IdTipoProducto == idTipoProducto);
            }

            return await Task.FromResult(query.ToList());
        }

        #endregion

        #region CUD

        public async Task AgregarProducto(Productos nuevoProducto)
        {
            var payload = JsonSerializer.Serialize(nuevoProducto);
            var jsonContent = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("api/Productos", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            await Task.CompletedTask;
        }

        public async Task EliminarProductoAsync(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/Productos/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            await Task.CompletedTask;
        }

        public async Task EditarProductoAsync(Productos productoActualizado)
        {
            var payload = JsonSerializer.Serialize(productoActualizado);
            var jsonContent = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PutAsync("api/Productos", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            await Task.CompletedTask;
        }

        #endregion
    }
}
