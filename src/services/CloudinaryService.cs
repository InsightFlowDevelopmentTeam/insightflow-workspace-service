using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.services
{
    /// <summary>
    /// Servicio para manejar operaciones con Cloudinary.
    /// </summary>
    public class CloudinaryService : ICloudinaryService
    {
        // Instancia de Cloudinary
        private readonly Cloudinary _cloudinary;
        /// <summary>
        /// Constructor del servicio Cloudinary.
        /// </summary>
        /// <param name="cloudinary"></param>
        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        /// <summary>
        /// Sube una imagen a Cloudinary.
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns>Una tarea que representa la operación asincrónica y contiene los parámetros de la imagen subida</returns>
        /// <exception cref="Exception"></exception>
        public Task<ImageParams> UploadImageAsync(IFormFile imageFile)
        {
            // Validar que el archivo no sea nulo
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
            // Configurar parámetros de subida
            ImageUploadParams uploadParams =
                new()
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    Folder = "insightflow/workspaces"
                };
            // Subir la imagen
            ImageUploadResult uploadResult = _cloudinary.Upload(uploadParams);
            // Manejar errores de subida
            if (uploadResult.Error != null)
            {
                throw new Exception(
                    "Error al subir la imagen: " + uploadResult.Error.Message
                );
            }
            // Retornar los parámetros de la imagen subida
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
    }
}