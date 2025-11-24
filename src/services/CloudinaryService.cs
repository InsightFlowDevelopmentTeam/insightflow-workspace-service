using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public Task<ImageParams> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                throw new Exception("El archivo de imagen no puede ser nulo");
            }
            // Validar tipo de archivo
            if (
                imageFile.ContentType != "image/png"
                && imageFile.ContentType != "image/jpg"
                && imageFile.ContentType != "image/jpeg"
                && imageFile.ContentType != "image/webp"
            )
            {
                throw new Exception("La imagen debe ser un PNG, JPG, JPEG o WEBP");
            }

            // Validar tamaño
            if (imageFile.Length > 5 * 1024 * 1024)
            {
                throw new Exception("El tamaño de la imagen no puede exceder 5 MB");
            }

            ImageUploadParams uploadParams =
                new()
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    Folder = "insightflow/workspaces"
                };

            ImageUploadResult uploadResult = _cloudinary.Upload(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(
                    "Error al subir la imagen: " + uploadResult.Error.Message
                );
            }

            ImageParams imageParams =
                new()
                {
                    Url = uploadResult.SecureUrl.ToString(),
                    PublicId = uploadResult.PublicId,
                    AltText = imageFile.FileName,
                    Width = uploadResult.Width,
                    Height = uploadResult.Height,
                    Format = uploadResult.Format,
                    Bytes = uploadResult.Bytes
                };
            return Task.FromResult(imageParams);
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new Exception(
                    "Public ID es requerido para eliminar la imagen"
                );
            }

            DeletionParams deletionParams = new(publicId);
            DeletionResult deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }

        public async Task<Dictionary<string, bool>> DeleteImagesAsync(IEnumerable<string> publicIds)
        {
            List<string> publicIdList = [.. publicIds];
            if (publicIdList.Count == 0)
            {
                return [];
            }

            DelResParams deletionParams = new() { PublicIds = publicIdList };

            DelResResult deletionResult = await _cloudinary.DeleteResourcesAsync(deletionParams);

            Dictionary<string, bool> result = [];

            // Cloudinary devuelve un diccionario con los resultados
            foreach (string publicId in publicIdList)
            {
                result[publicId] =
                    deletionResult.Deleted.ContainsKey(publicId)
                    && deletionResult.Deleted[publicId] == "deleted";
            }

            return result;
        }
    }
}