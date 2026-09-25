namespace Productos.API.Modal.BaseDatos.Core
{
    public class SP
    {
        public struct SQL
        {
            public const string InsertaProducto = "Usp_InsertaProducto";
            public const string ActualizaProducto = "Usp_ActualizaProducto";
            public const string EliminaProducto = "Usp_EliminaProducto";
            public const string Login = "Usp_Login";
        }

        public struct Query
        {
            public const string QueryProductos = "select * from dbo.ProductosView";
            public const string QueryProveedores = "select * from dbo.ProveedoresView";
            public const string QueryTipos = "select * from dbo.TiposView";
        }
    }
}
