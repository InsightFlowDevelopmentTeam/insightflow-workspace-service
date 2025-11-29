using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.models
{
    /// <summary>
    /// Modelo de usuario dentro de un workspace.
    /// </summary>
    public class User
    {
        // ID del usuario
        public Guid Id { get; set; }
        // Nombre del usuario
        public string Name { get; set; } = string.Empty;
        // Rol del usuario dentro del workspace
        public string Role { get; set; } = string.Empty;
    }
}