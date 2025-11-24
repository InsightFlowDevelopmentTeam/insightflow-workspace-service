using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.interfaces
{
    public interface ICloudinaryService
    {
        Task<ImageParams> UploadImageAsync(IFormFile imageFile);
        Task<bool> DeleteImageAsync(string publicId);
        Task<Dictionary<string, bool>> DeleteImagesAsync(IEnumerable<string> publicIds);
    }
}