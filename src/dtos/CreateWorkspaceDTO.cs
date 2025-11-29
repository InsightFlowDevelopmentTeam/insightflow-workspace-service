using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    /// <summary>
    /// DTO para la creación de un nuevo workspace.
    /// </summary>
    public class CreateWorkspaceDTO
    {
        // Nombre del workspace
        [Required]
        public string Name { get; set; } = string.Empty;
        // Descripción del workspace
        [Required]
        public string Description { get; set; } = string.Empty;
        // Tema del workspace
        [Required]
        public string Topic { get; set; } = string.Empty;
        // Imagen del workspace
        [Required]
        public IFormFile? Image { get; set; }
        // ID del propietario del workspace
        [Required]
        public Guid OwnerId { get; set; }
        // Nombre del propietario del workspace
        [Required]
        public string OwnerName { get; set; } = string.Empty;
    }
}