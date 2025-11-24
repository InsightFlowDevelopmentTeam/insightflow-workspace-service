using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.interfaces
{
    public interface IWorkspaceRepository
    {
        Task<bool> CreateWorkspaceAsync(CreateWorkspaceDTO createWorkspaceDTO);
        Task<IEnumerable<Workspace>> GetAllWorkspacesByUserAsync(Guid userId);
        Task<IEnumerable<Workspace>> GetAllWorkspacesAsync();
    }
}