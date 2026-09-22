using Microsoft.AspNetCore.Mvc;
using Productos.API.Modal.BaseDatos.DAO;

namespace Productos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityDAO _securityDAO;
        public SecurityController(ISecurityDAO securityDAO)
        {
            _securityDAO = securityDAO;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Modal.Entidades.LoginRequest loginRequest)
        {
            var login = await _securityDAO.Login(loginRequest);
            if (!login) return BadRequest("Usuario y/o contraseña incorrectos");
            return Ok(new { mensaje = "Login exitoso." });
        }
    }
}
