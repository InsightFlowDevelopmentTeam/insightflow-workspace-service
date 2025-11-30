using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.mappers
{
    /// <summary>
    /// Mapper estático para workspaces.
    /// Realiza conversiones entre DTOs y modelos.
    /// </summary>
    public static class WorkspaceMapper
    {
        /// <summary>
        /// Convierte un CreateWorkspaceDTO a un modelo Workspace.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageUrl"></param>
        /// <returns> Workspace </returns>
        public static Workspace ToWorkspace(this CreateWorkspaceDTO dto, string imageUrl)
        {
            // Crear el usuario propietario del workspace
            User user = new User
            {
                Id = dto.OwnerId,
                Name = dto.OwnerName,
                Role = "Owner"
            };
            // Crear el workspace
            return new Workspace
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Topic = dto.Topic,
                ImageUrl = imageUrl,
                Users = new List<User> { user },
                // Establecer la fecha de creación
                CreatedAt = DateTime.UtcNow
            };
        }
        /// <summary>
        /// Convierte un modelo Workspace a un GetWorkspaceDTO.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns> GetWorkspaceDTO </returns>
        public static GetWorkspaceDTO ToGetWorkspaceDTO(Workspace workspace)
        {
            // Mapear las propiedades del workspace al DTO
            return new GetWorkspaceDTO
            {
                Name = workspace.Name,
                Url = workspace.ImageUrl,
                Users = workspace.Users,
                CreatedAt = workspace.CreatedAt
            };
        }
        /// <summary>
        /// Convierte una lista de Workspace a una lista de GetByUserId.
        /// </summary>
        /// <param name="workspaces"></param>
        /// <param name="userId"></param>
        /// <returns> Lista de GetByUserId </returns>
        public static List<GetByUserId> ToGetByUserIdList(List<Workspace> workspaces, Guid userId)
        {
            // Mapear cada workspace al DTO correspondiente
            var result = new List<GetByUserId>();
            foreach (var workspace in workspaces)
            {
                var dto = new GetByUserId
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    ImageUrl = workspace.ImageUrl,
                    UserRole = workspace.Users.FirstOrDefault(u => u.Id == userId)?.Role ?? string.Empty
                };
                result.Add(dto);
            }
            // Devolver la lista de DTOs
            return result;
        }
    }
}