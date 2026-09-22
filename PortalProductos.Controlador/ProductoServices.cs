using PortalProductos.Modelo;
using PortalProductos.Modelo.Entidades;
using System.Net.Http.Json;
using System.Text.Json;

namespace PortalProductos.Controlador
{
    public class ProductoServices : IProductoServices
    {
        private static List<Productos> _listaProductos = new List<Productos>();
        private readonly HttpClient _client;
        public ProductoServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Productos>> ObtieneProductosAsync()
        {
            var response = await _client.GetAsync("api/Productos");
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

        public async Task<List<Productos>> ConsultaDinamica(string? nombre, string? idCliente)
        {
            
            var query = _listaProductos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(q => q.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(idCliente))
            {
                query = query.Where(q => q.Nombre.Contains(idCliente, StringComparison.OrdinalIgnoreCase));
            }

            return await Task.FromResult(query.ToList());
        }

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
                var response = await _client.DeleteAsync($"Productos/{id}");
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
    }
}
