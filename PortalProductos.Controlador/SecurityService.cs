namespace PortalProductos.Controlador
{
    public class SecurityService : ISecurityService
    {
        public bool isAuth { get; set; }
        public string? UsuarioActual { get; private set; }
        public event Func<Task>? OnAuthStateChanged;

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (username == "Admin" && password == "1234")
            {
                isAuth = true;
                UsuarioActual = username;
                if (OnAuthStateChanged != null)
                {
                    await OnAuthStateChanged.Invoke();
                }
                return true;
            }
            return false;
        }

        public void Logout()
        {
            isAuth = false;
            UsuarioActual = null;
            OnAuthStateChanged?.Invoke();
        }
    }
}
