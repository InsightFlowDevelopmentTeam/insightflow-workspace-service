using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    /// <summary>
    /// DTO para actualizar la información de un workspace.
    /// </summary>
    public class UpdateWorkspaceDTO
    {
        // Nombre del workspace
        [Required]
        public string Name { get; set; } = string.Empty;
        // Imagen del workspace (opcional)
        public IFormFile? Image { get; set; }
    }
}