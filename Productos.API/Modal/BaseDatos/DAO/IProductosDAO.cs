using Productos.API.Modal.Entidades;

namespace Productos.API.Modal.BaseDatos.DAO
{
    public interface IProductosDAO
    {
        Task<List<Entidades.Productos>> ObtenerProductosAsync();
        Task<bool> InsertaProductoAsync(Entidades.Productos producto);
        Task<bool> ActualizaProductoAsync(Entidades.Productos producto);
        Task<bool> EliminaProductoAsync(int id);
    }
}
