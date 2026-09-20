using PortalProductos.Modelo;
using PortalProductos.Modelo.Entidades;

namespace PortalProductos.Controlador
{
    public interface IProductoServices
    {
        Task<List<Productos>> ObtieneProductosAsync();
        Task<List<Productos>> ConsultaDinamica(string? nombre, string? idCliente);
        Task AgregarProducto(Productos nuevoProducto);
        Task EliminarProductoAsync(int id);
        Task EditarProductoAsync(Productos productoActualizado);
    }
}
