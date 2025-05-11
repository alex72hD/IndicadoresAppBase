using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IndicadoresApp;
using IndicadoresApp.Services;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración mejorada del HttpClient
builder.Services.AddScoped(sp => {
    // Importante: Apuntamos directamente al servidor correcto
    // Utilizamos la misma URL a la que se está redirigiendo
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7237/api/")
    };

    // Configurar el HttpClient para manejar peticiones CORS correctamente
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    // Configurar para usar cookies/credenciales
 

    return httpClient;
});

builder.Services.AddScoped<ServicioEntidad>();
builder.Services.AddScoped<ValidacionAcceso>();

await builder.Build().RunAsync();