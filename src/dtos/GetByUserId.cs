using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.dtos
{
    public class GetByUserId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}