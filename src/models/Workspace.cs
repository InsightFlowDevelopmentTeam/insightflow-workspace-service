using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.models
{
    /// <summary>
    /// Modelo de workspace.
    /// </summary>
    public class Workspace
    {
        // ID del workspace en UUID V4
        public Guid Id { get; set; }
        // Nombre del workspace
        public string Name { get; set; } = string.Empty;
        // Descripción del workspace
        public string Description { get; set; } = string.Empty;
        // Temática del workspace
        public string Topic { get; set; } = string.Empty;
        // URL de la imagen del workspace
        public string ImageUrl { get; set; } = string.Empty;
        // Lista de usuarios asociados al workspace
        public List<User> Users { get; set; } = new List<User>();
        // Fecha de creación del workspace
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Indica si el workspace está activo o no (Soft Delete)
        public bool IsActive { get; set; } = true;
    }
}