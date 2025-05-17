// Se importan las dependencias necesarias
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IndicadoresApp.Services
{
    // Definir la clase base para validar el acceso a las páginas
    public class ValidacionAcceso : ComponentBase
    {
        // Inyectar servicio de interoperabilidad con JavaScript
        [Inject] protected IJSRuntime InteropJS { get; set; } = default!;

        // Inyectar servicio para manejar la navegación
        [Inject] protected NavigationManager Navegacion { get; set; } = default!;

        // Controlar si el acceso está permitido
        private bool accesoPermitido = false;

        // Sobrescribir el método que se ejecuta después del primer renderizado
        protected override async Task OnAfterRenderAsync(bool primerRenderizado)
        {
            // Mejorable: en el futuro considerar validar acceso antes de renderizar para mejorar la UX
            if (primerRenderizado) await ValidarAccesoAsync();
            await base.OnAfterRenderAsync(primerRenderizado);
        }

        // Controlar si el componente debe ser renderizado
        protected override bool ShouldRender()
        {
            // Obtener la ruta actual eliminando la base URL y los parámetros
            var ruta = Navegacion.Uri.Replace(Navegacion.BaseUri, "/").Split('?')[0];

            // Permitir renderizado solo si está en login o si el acceso fue validado
            return ruta.Equals("/login", StringComparison.OrdinalIgnoreCase) || accesoPermitido;
        }

        // Validar el acceso del usuario a la página
        private async Task ValidarAccesoAsync()
        {
            try
            {
                var ruta = Navegacion.Uri.Replace(Navegacion.BaseUri, "/").Split('?')[0];

                // Permitir acceso inmediato si se está en la página de login
                if (ruta.Equals("/login", StringComparison.OrdinalIgnoreCase))
                {
                    accesoPermitido = true;
                    return;
                }

                // Obtener el email del usuario y rol desde sessionStorage
                var correoUsuario = await InteropJS.InvokeAsync<string>("sessionStorage.getItem", "usuarioEmail");
                var rolUsuario = await InteropJS.InvokeAsync<string>("sessionStorage.getItem", "rolUsuario");

                // Redirigir a login si no hay correo en sesión
                if (string.IsNullOrEmpty(correoUsuario))
                {
                    await InteropJS.InvokeVoidAsync("alert", "Sesión no válida. Redirigiendo al login...");
                    Navegacion.NavigateTo("/login", true);
                    return;
                }

                // Si el usuario es administrador, permitir acceso a todas las rutas
                if (!string.IsNullOrEmpty(rolUsuario) && rolUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    accesoPermitido = true;
                    StateHasChanged();
                    return;
                }

                // Para usuarios no administradores, verificar permisos específicos
                var rutasPermitidas = await InteropJS.InvokeAsync<List<string>>("eval", @"
                    Object.keys(sessionStorage)
                          .filter(k => k.startsWith('ruta_'))
                          .map(k => sessionStorage.getItem(k))");

                // Verificar si la ruta actual está permitida para el usuario
                if (!rutasPermitidas.Contains(ruta))
                {
                    await InteropJS.InvokeVoidAsync("alert", "No tiene permisos para acceder a esta página.");
                    Navegacion.NavigateTo("/", true);
                    return;
                }

                // Permitir el acceso si todas las validaciones pasan
                accesoPermitido = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // Mejorado: capturar el error con más detalle
                Console.WriteLine($"Error en validación de acceso: {ex.Message}");
                await InteropJS.InvokeVoidAsync("alert", "Error en la validación de acceso.");
                Navegacion.NavigateTo("/", true);
            }
        }
    }
}
