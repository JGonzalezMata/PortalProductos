namespace PortalProductos.Controlador
{
    public interface ISecurityService
    {
        bool isAuth {  get; }
        string? UsuarioActual {  get; }
        event Func<Task>? OnAuthStateChanged;

        Task<bool> LoginAsync(string username, string password);
        void Logout();
    }
}
