namespace Productos.API.Modal.BaseDatos.DAO
{
    public interface ISecurityDAO
    {
        Task<bool> Login(Modal.Entidades.LoginRequest loginRequest);
    }
}
