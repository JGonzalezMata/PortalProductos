namespace PortalProductos.Controlador
{
    public interface ISecurityService
    {
        bool isAuth {  get; }
        string? UsuarioActual {  get; }
        event Func<Task>? OnAuthStateChanged;

        Task<bool> LoginAsync(Modelo.Entidades.LoginRequest loginRequest);
        void Logout();
    }
}
