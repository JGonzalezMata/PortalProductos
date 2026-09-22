using Productos.API.Modal.Entidades;

namespace Productos.API.Controllers
{
    public class SecurityService
    {
        Task<List<Productos>> ObtieneProductosAsync();
        Task<List<Productos>> ConsultaDinamica(string? nombre, string? idCliente);
        Task AgregarProducto(Productos nuevoProducto);
        Task EliminarProductoAsync(int id);
        Task EditarProductoAsync(Productos productoActualizado);
    }
}
