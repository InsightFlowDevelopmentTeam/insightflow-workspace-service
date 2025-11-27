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
        public static GetWorkspaceDTO ToGetWorkspaceDTO(Workspace workspace)
        {
            return new GetWorkspaceDTO
            {
                Name = workspace.Name,
                Url = workspace.ImageUrl,
                Users = workspace.Users,
                CreatedAt = workspace.CreatedAt
            };
        }
        public static List<GetByUserId> ToGetByUserIdList(List<Workspace> workspaces)
        {
            var result = new List<GetByUserId>();
            foreach (var workspace in workspaces)
            {
                var dto = new GetByUserId
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    ImageUrl = workspace.ImageUrl,
                    UserRole = workspace.Users.FirstOrDefault()?.Role ?? string.Empty
                };
                result.Add(dto);
            }
            return result;
        }
    }
}