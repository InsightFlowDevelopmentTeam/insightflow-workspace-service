using System.Collections;
using CloudinaryDotNet;
using insightflow_workspace_service.src.data;
using insightflow_workspace_service.src.dtos;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.mappers;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.repositories
{
    /// <summary>
    /// Repositorio de workspaces 
    /// Utiliza un servicio de cloudinary para el manejo de imágenes.
    /// </summary>
    public class WorkspaceRepository : IWorkspaceRepository
    {
        // Servicio de cloudinary
        private readonly ICloudinaryService _cloudinaryService;
        /// <summary>
        /// Constructor del repositorio de workspaces.
        /// </summary>
        /// <param name="cloudinaryService">Servicio de cloudinary para manejo de imágenes.</param>
        public WorkspaceRepository(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        // Lista estática de workspaces obtenidos del DataContext
        List<Workspace> workspaces = DataContext.Workspaces;
        /// <summary>
        /// Crea un nuevo workspace.
        /// </summary>
        /// <param name="createWorkspaceDTO"></param>
        /// <returns> True si se crea el workspace, false si ya existe uno con el mismo nombre o si la imagen no se puede subir.|</returns>
        public Task<bool> CreateWorkspaceAsync(CreateWorkspaceDTO createWorkspaceDTO)
        {
            // Verificar si ya existe un workspace con el mismo nombre
            if (workspaces.Find(w => w.Name == createWorkspaceDTO.Name) != null)
            {
                return Task.FromResult(false);
            }
            // Subir la imagen al servicio de cloudinary
            var uploadResult = _cloudinaryService.UploadImageAsync(createWorkspaceDTO.Image!).Result;
            if (uploadResult == null)
            {
                return Task.FromResult(false);
            }
            // Mapear el DTO a un modelo de workspace y agregarlo a la lista
            var newWorkspace = WorkspaceMapper.ToWorkspace(createWorkspaceDTO, uploadResult.Url.ToString());
            workspaces.Add(newWorkspace);
            // Retornar true indicando que el workspace fue creado exitosamente
            return Task.FromResult(true);
        }
        /// <summary>
        /// Obtiene todos los workspaces asociados a un usuario por su ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Lista de workspaces asociados al usuario.</returns>
        public Task<IEnumerable<GetByUserId>> GetAllWorkspacesByUserAsync(Guid userId)
        {
            // Filtrar los workspaces que contienen al usuario con el ID proporcionado
            var userWorkspaces = new List<Workspace>();
            foreach (var workspace in workspaces)
            {
                foreach (var user in workspace.Users)
                {
                    if (userId == user.Id && workspace.IsActive)
                    {
                        userWorkspaces.Add(workspace);
                    }
                }
            }
            // Mapear los workspaces a DTOs y retornarlos
            List<GetByUserId> result = WorkspaceMapper.ToGetByUserIdList(userWorkspaces, userId);
            return Task.FromResult(result.AsEnumerable());
        }
        /// <summary>
        /// Obtiene un workspace por su ID.
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        public async Task<GetWorkspaceDTO> GetWorkspaceByIdAsync(Guid workspaceId)
        {
            // Buscar el workspace por su ID
            var workspace = workspaces.Find(w => w.Id == workspaceId);
            // Verificar si el workspace existe y está activo
            if (workspace == null)
            {
                return null!;
            }
            if (workspace.IsActive == false)
            {
                return null!;
            }
            // Mapear el workspace a un DTO y retornarlo
            var getWorkspaceDTO = WorkspaceMapper.ToGetWorkspaceDTO(workspace);
            return getWorkspaceDTO;
        }
        /// <summary>
        /// Actualiza un workspace por su ID.
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <param name="updateWorkspaceDTO"></param>
        /// <returns> True si se actualiza el workspace, false si no existe, está inactivo o hay conflicto de nombre.</returns>
        public Task<bool> UpdateWorkspaceAsync(Guid workspaceId, UpdateWorkspaceDTO updateWorkspaceDTO)
        {
            // Buscar el workspace por su ID
            var workspace = workspaces.Find(w => w.Id == workspaceId);
            // Verificar si el workspace existe y está activo
            if (workspace == null)
            {
                return Task.FromResult(false);
            }
            if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            }
            // Verificar si hay conflicto de nombre con otros workspaces activos y no con el mismo
            foreach (var w in workspaces)
            {
                if (w.Name.Equals(updateWorkspaceDTO.Name) && w.Id != workspaceId)
                {
                    return Task.FromResult(false);
                }
            }
            // Actualizar la imagen si se proporciona una nueva
            var url = workspaces.Find(w => w.Id == workspaceId)!.ImageUrl;
            if (updateWorkspaceDTO.Image != null)
            {
                // Subir la nueva imagen al servicio de Cloudinary
                var uploadResult = _cloudinaryService.UploadImageAsync(updateWorkspaceDTO.Image!).Result;
                if (uploadResult == null)
                {
                    return Task.FromResult(false);
                }
                // Actualizar la URL de la imagen con la nueva URL subida
                url = uploadResult.Url.ToString();
            }
            // Actualizar el nombre y la imagen del workspace
            workspaces.Find(w => w.Id == workspaceId)!.Name = updateWorkspaceDTO.Name;
            workspaces.Find(w => w.Id == workspaceId)!.ImageUrl = url;
            return Task.FromResult(true);
        }
        /// <summary>
        /// Elimina un workspace por su ID.
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns> True si se elimina el workspace, false si no existe o está inactivo.</returns>
        public Task<bool> DeleteWorkspaceAsync(Guid workspaceId)
        {
            // Buscar el workspace por su ID
            var workspace = workspaces.Find(w => w.Id == workspaceId);
            // Verificar si el workspace existe y está activo
            if (workspace == null)
            {
                return Task.FromResult(false);
            }
            if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            }
            // Marcar el workspace como inactivo en lugar de eliminarlo físicamente (soft delete)
            workspaces.Find(w => w.Id == workspaceId)!.IsActive = false;
            // Retornar true indicando que el workspace fue eliminado exitosamente
            return Task.FromResult(true);
        }
        /// <summary>
        /// Obtiene todos los workspaces.
        /// Esta función es principalmente para propósitos de depuración.
        /// </summary>
        /// <returns>Una lista de todos los workspaces.</returns>
        public Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
        {
            // Retornar todos los workspaces
            return Task.FromResult(workspaces.AsEnumerable());
        }
    }
}