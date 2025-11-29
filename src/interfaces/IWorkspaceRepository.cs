using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de workspaces.
    /// </summary>
    public interface IWorkspaceRepository
    {
        // Crear un nuevo workspace
        Task<bool> CreateWorkspaceAsync(CreateWorkspaceDTO createWorkspaceDTO);
        // Obtener todos los workspaces de un usuario
        Task<IEnumerable<GetByUserId>> GetAllWorkspacesByUserAsync(Guid userId);
        // Obtener un workspace por su ID
        Task<GetWorkspaceDTO> GetWorkspaceByIdAsync(Guid workspaceId);
        // Actualizar un workspace
        Task<bool> UpdateWorkspaceAsync(Guid workspaceId, UpdateWorkspaceDTO updateWorkspaceDTO);
        // Eliminar un workspace
        Task<bool> DeleteWorkspaceAsync(Guid workspaceId);
        // Obtener todos los workspaces (Funcion solo para depuración)
        Task<IEnumerable<Workspace>> GetAllWorkspacesAsync();
    }
}