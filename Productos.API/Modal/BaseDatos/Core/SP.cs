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

            public const string ObtieneProductos = "Usp_ObtieneProductos";
            public const string ObtieneProveedores = "Usp_ObtieneProveedores";
            public const string ObtieneTipos = "Usp_ObtieneTipos";
        }
    }
}
