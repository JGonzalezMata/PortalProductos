using PortalProductos.Controlador;
using PortalProductos.Vista.Components;
using System.Net.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Registro de dependencias
builder.Services.AddScoped<IProductoServices, ProductoServices>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => {
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => true
    };

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:4849/")
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
