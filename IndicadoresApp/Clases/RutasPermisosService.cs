namespace IndicadoresApp.Clases
{
    namespace IndicadoresApp.Clases
    {
        public class RutasPermisosService
        {
            // Lista con todas las rutas del sistema
            public static readonly List<string> TodasLasRutas = new()
            {
                "/tipoindicador",
                "/sentido",
                "/indicador",
                "/variable",
                "/variableporindicador",
                "/actor",
                "/tipoactor",
                "/rol",
                "/rolusuario",
                "/fuente",
                "/fuentesporindicador",
                "/frecuencia",
                "/unidadmedicion",
                "/resultadoindicador",
                "/representante",
                "/representantevisuales",
                "/responsableporindicador",
                "/usuario",
                "/prueba-roles-rutas",
                "/gestion-rutas"
            };

            // Rutas relacionadas con usuarios que se excluirán para el rol "verificador" y "validador" 
            public static readonly List<string> RutasUsuarios = new()
            {
                "/usuario",
                "/prueba-roles-rutas",
                "/gestion-rutas",
                "/actor",
                "/tipoactor",
                "/rol",
                "/rolusuario"
            };

            /// <summary>
            /// Verifica si un usuario debe tener acceso a todas las rutas basado en su rol
            /// </summary>
            public static bool EsRolTodosAccesos(string? rol)
            {
                // Los roles que tienen acceso a todas las rutas
                return !string.IsNullOrEmpty(rol) &&
                      (rol.Equals("admin", StringComparison.OrdinalIgnoreCase) ||
                       rol.Equals("superadmin", StringComparison.OrdinalIgnoreCase));
            }

            /// <summary>
            /// Verifica si un usuario tiene el rol "verificador"
            /// </summary>
            public static bool EsRolVerificador(string? rol)
            {
                return !string.IsNullOrEmpty(rol) &&
                       rol.Equals("verificador", StringComparison.OrdinalIgnoreCase);
            }

            /// <summary>
            /// Verifica si un usuario tiene el rol "validador"
            /// </summary>
            public static bool EsRolValidador(string? rol)
            {
                return !string.IsNullOrEmpty(rol) &&
                       rol.Equals("validador", StringComparison.OrdinalIgnoreCase);
            }

            /// <summary>
            /// Verifica si un usuario tiene rol "verificador" o "validador" (ambos tienen los mismos permisos)
            /// </summary>
            public static bool EsRolVerificadorOValidador(string? rol)
            {
                return EsRolVerificador(rol) || EsRolValidador(rol);
            }

            /// <summary>
            /// Obtiene las rutas permitidas para los roles "verificador" y "validador"
            /// </summary>
            public static List<string> ObtenerRutasVerificador()
            {
                return TodasLasRutas.Where(ruta => !RutasUsuarios.Contains(ruta)).ToList();
            }

            /// <summary>
            /// Obtiene las rutas permitidas para el rol "validador" (mismas que verificador)
            /// </summary>
            public static List<string> ObtenerRutasValidador()
            {
                return ObtenerRutasVerificador();
            }
        }
    }
}