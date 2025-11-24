using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.mappers
{
    public static class WorkspaceMapper
    {
        public static Workspace ToWorkspace(this CreateWorkspaceDTO dto, string imageUrl)
        {
            User user = new User
            {
                Id = dto.OwnerId,
                Name = dto.OwnerName,
                Role = "Owner"
            };
            return new Workspace
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Topic = dto.Topic,
                ImageUrl = imageUrl,
                Users = new List<User> { user },
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}