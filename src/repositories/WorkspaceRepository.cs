using System.Collections;
using CloudinaryDotNet;
using insightflow_workspace_service.src.data;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.mappers;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ICloudinaryService _cloudinaryService;

        public WorkspaceRepository(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        List<Workspace> workspaces = DataContext.Workspaces;
        public Task<bool> CreateWorkspaceAsync(CreateWorkspaceDTO createWorkspaceDTO)
        {
            if (workspaces.Find(w => w.Name == createWorkspaceDTO.Name) != null)
            {
                return Task.FromResult(false);
            }
            var uploadResult = _cloudinaryService.UploadImageAsync(createWorkspaceDTO.Image!).Result;
            if (uploadResult == null)
            {
                return Task.FromResult(false);
            }
            var newWorkspace = WorkspaceMapper.ToWorkspace(createWorkspaceDTO, uploadResult.Url.ToString());
            workspaces.Add(newWorkspace);
            return Task.FromResult(true);
        }
        public Task<IEnumerable<Workspace>> GetAllWorkspacesByUserAsync(Guid userId)
        {
            var userWorkspaces = new List<Workspace>();
            foreach (var workspace in workspaces)
            {
                foreach (var user in workspace.Users)
                {
                    if (userId == user.Id)
                    {
                        userWorkspaces.Add(workspace);
                    }
                }
            }
            return Task.FromResult(userWorkspaces.AsEnumerable());
        }
        public async Task<GetWorkspaceDTO> GetWorkspaceByIdAsync(Guid workspaceId)
        {
            var workspace = workspaces.Find(w => w.Id == workspaceId);
            if (workspace == null)
            {
                return null!;
            }
            if (workspace.IsActive == false)
            {
                return null!;
            }
            var getWorkspaceDTO = WorkspaceMapper.ToGetWorkspaceDTO(workspace);
            return getWorkspaceDTO;
        }
        public Task<bool> UpdateWorkspaceAsync(Guid workspaceId, UpdateWorkspaceDTO updateWorkspaceDTO)
        {
            var workspace = workspaces.Find(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return Task.FromResult(false);
            }
            if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            }
            if (workspace.Name == updateWorkspaceDTO.Name)
            {
                return Task.FromResult(false);
            }
            var url = workspaces.Find(w => w.Id == workspaceId)!.ImageUrl;
            if (updateWorkspaceDTO.Image != null)
            {
                var uploadResult = _cloudinaryService.UploadImageAsync(updateWorkspaceDTO.Image!).Result;
                if (uploadResult == null)
                {
                    return Task.FromResult(false);
                }
                url = uploadResult.Url.ToString();
            }
            workspaces.Find(w => w.Id == workspaceId)!.Name = updateWorkspaceDTO.Name;
            workspaces.Find(w => w.Id == workspaceId)!.ImageUrl = url;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteWorkspaceAsync(Guid workspaceId)
        {
            var workspace = workspaces.Find(w => w.Id == workspaceId);
            if (workspace == null)
            {
                return Task.FromResult(false);
            }
            if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            }
            workspaces.Find(w => w.Id == workspaceId)!.IsActive = false;
            return Task.FromResult(true);
        }
        // solo debug
        public Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
        {
            return Task.FromResult(workspaces.AsEnumerable());
        }
    }
}