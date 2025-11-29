using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.interfaces
{
    /// <summary>
    /// Interfaz para el servicio de Cloudinary.
    /// </summary>
    public interface ICloudinaryService
    {
        // Subir una imagen a Cloudinary
        Task<ImageParams> UploadImageAsync(IFormFile imageFile);
    }
}