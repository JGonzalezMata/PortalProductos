using PortalProductos.Modelo;
using PortalProductos.Modelo.Entidades;

namespace PortalProductos.Controlador
{
    public class ProductoServices : IProductoServices
    {
        private readonly HttpClient _client;
        public ProductoServices(HttpClient client)
        {
            _client = client;
        }
        private static List<Productos> _listaProductos = new List<Productos>
        {
            new Productos { Id = 1, Nombre = "Laptop", Precio = 1200, idCliente = "01Laptop" },
                new Productos { Id = 2, Nombre = "Desktop", Precio = 2400, idCliente = "01Desktop" }
        };
        public async Task<List<Productos>> ObtieneProductosAsync()
        {
            return await Task.FromResult(_listaProductos);
        }

        public async Task AgregarProducto(Productos nuevoProducto)
        {
            nuevoProducto.Id = _listaProductos.Max(val =>  val.Id) + 1;
            _listaProductos.Add(nuevoProducto);
            await Task.CompletedTask;
        }

        public async Task<List<Productos>> ConsultaDinamica(string? nombre, string? idCliente)
        {
            var respnse = await _client.GetAsync("https://localhost:4849/weatherforecast");
            var content = await respnse.Content.ReadAsStringAsync();
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

        public async Task EliminarProductoAsync(int id)
        {
            var producto = _listaProductos.FirstOrDefault(q => q.Id == id);
            if (producto != null)
            {
                _listaProductos.Remove(producto);
            }
            await Task.CompletedTask;
        }

        public async Task EditarProductoAsync(Productos productoActualizado)
        {
            var exists = _listaProductos.FirstOrDefault(q => q.Id == productoActualizado.Id);
            if (exists != null)
            {
                exists.Nombre = productoActualizado.Nombre;
                exists.Precio = productoActualizado.Precio;
                exists.idCliente = productoActualizado.idCliente;
            }

            await Task.CompletedTask;
        }
    }
}
