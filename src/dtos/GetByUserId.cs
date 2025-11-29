using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    /// <summary>
    /// DTO para obtener workspaces por ID de usuario.
    /// </summary>
    public class GetByUserId
    {
        // ID del workspace
        public Guid Id { get; set; }
        // Nombre del workspace
        public string Name { get; set; } = string.Empty;
        // URL de la imagen del workspace
        public string ImageUrl { get; set; } = string.Empty;
        // Rol del usuario en el workspace
        public string UserRole { get; set; } = string.Empty;
    }
}