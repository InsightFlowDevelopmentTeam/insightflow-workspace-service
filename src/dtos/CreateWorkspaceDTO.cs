using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    public class CreateWorkspaceDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Topic { get; set; } = string.Empty;
        [Required]
        public IFormFile? Image { get; set; }
        //[Required]
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
    }
}