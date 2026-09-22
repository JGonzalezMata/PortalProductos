using System.Text.Json;
using PortalProductos.Modelo.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace PortalProductos.Controlador
{
    public class SecurityService : ISecurityService
    {
        public bool isAuth { get; set; }
        public string? UsuarioActual { get; private set; }
        public event Func<Task>? OnAuthStateChanged;
        private readonly HttpClient _client;

        public SecurityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            //loginRequest.password = HashPassword(loginRequest.username, loginRequest.password);
            var payload = JsonSerializer.Serialize(loginRequest);
            var jsonContent = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync("https://localhost:4849/api/Security", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    isAuth = true;
                    UsuarioActual = loginRequest.username;
                    if (OnAuthStateChanged != null)
                    {
                        await OnAuthStateChanged.Invoke();
                    }
                    return true;
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Logout()
        {
            isAuth = false;
            UsuarioActual = null;
            OnAuthStateChanged?.Invoke();
        }

        private static string HashPassword(string user, string password)
        {
            var hasher = new PasswordHasher<string>();
            return hasher.HashPassword(user,password);
        }
    }
}
