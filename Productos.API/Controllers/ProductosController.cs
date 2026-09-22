using Microsoft.AspNetCore.Mvc;
using Productos.API.Modal.BaseDatos.DAO;
using Productos.API.Modal.Entidades;

namespace Productos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosDAO _productosDAO;

        public ProductosController(IProductosDAO productosDAO)
        {
            _productosDAO = productosDAO;
        }

        [HttpGet]
        public async Task<IActionResult> ObtieneProductos()
        {
            var productos = await _productosDAO.ObtenerProductosAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> InsertaProducto([FromBody] Modal.Entidades.Productos producto)
        {
            try
            {
                var resultado = await _productosDAO.InsertaProductoAsync(producto);
                if (!resultado) return BadRequest("No se pudo insertar el producto.");
                return Ok(new { mensaje = "Producto almacenado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizaProducto([FromBody] Modal.Entidades.Productos producto)
        {
            try
            {
                var resultado = await _productosDAO.ActualizaProductoAsync(producto);
                if (!resultado) return BadRequest("No se pudo actualizar el producto.");
                return Ok(new { mensaje = "Producto actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var resultado = await _productosDAO.EliminaProductoAsync(id);
                if (!resultado) return BadRequest("Producto no fue encontrado.");
                return Ok(new { mensaje = "Producto eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
