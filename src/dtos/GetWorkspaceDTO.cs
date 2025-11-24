using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.dtos
{
    public class GetWorkspaceDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new List<User>();
        public DateTime CreatedAt { get; set; }
    }
}