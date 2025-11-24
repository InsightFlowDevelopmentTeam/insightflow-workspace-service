using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.data
{
    public static class DataContext
    {
        public static List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}