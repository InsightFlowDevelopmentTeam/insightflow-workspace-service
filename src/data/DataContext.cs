using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.data
{
    /// <summary>
    /// Contexto de datos para los workspaces.
    /// Utiliza una lista estática para almacenar los workspaces en memoria.
    /// </summary>
    public static class DataContext
    {
        /// <summary>
        /// Lista estática de workspaces.
        /// </summary>
        public static List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}