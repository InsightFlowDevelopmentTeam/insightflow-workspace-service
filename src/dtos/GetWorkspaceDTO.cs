using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.dtos
{
    /// <summary>
    /// DTO para obtener la información de un workspace según su ID.
    /// </summary>
    public class GetWorkspaceDTO
    {
        // Nombre del workspace
        public string Name { get; set; } = string.Empty;
        // URL de la imagen del workspace
        public string Url { get; set; } = string.Empty;
        // Descripción del workspace
        public List<User> Users { get; set; } = new List<User>();
        // Fecha de creación del workspace
        public DateTime CreatedAt { get; set; }
    }
}