using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.helpers
{
    /// <summary>
    /// Configuración para Cloudinary.
    /// </summary>
    public class CloudinarySettings
    {
        // Nombre del cloud
        public string CloudName { get; set; } = string.Empty;
        // Clave de la API
        public string ApiKey { get; set; } = string.Empty;
        // Secreto de la API
        public string ApiSecret { get; set; } = string.Empty;
    }
    
}