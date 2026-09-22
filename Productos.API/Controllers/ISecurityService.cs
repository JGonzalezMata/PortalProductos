namespace Productos.API.Controllers
{
    public interface ISecurityService
    {
        Task<bool> LoginAsync(string username, string password);
        void Logout();
    }
}
