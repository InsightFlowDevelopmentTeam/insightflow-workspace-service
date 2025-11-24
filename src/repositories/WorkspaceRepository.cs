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
        public Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
        {
            return Task.FromResult(workspaces.AsEnumerable());
        }
    }
}