namespace Productos.API.Modal.Entidades
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal CostoReventa { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string EmpresaProveedor { get; set; }
        public int IdTipoProducto { get; set; }
        public string NombreTipoProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string IdProductoProveedor { get; set; }
    }
}
