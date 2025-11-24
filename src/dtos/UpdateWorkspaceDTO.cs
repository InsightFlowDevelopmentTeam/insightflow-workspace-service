using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    public class UpdateWorkspaceDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}