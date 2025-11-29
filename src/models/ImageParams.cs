using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.models
{
    /// <summary>
    /// Parámetros de una imagen para uso de cloudinary.
    /// </summary>
    public class ImageParams
    {
        // Propiedades de la imagen
        public string Url { get; set; } = string.Empty;
        // ID público de la imagen
        public string PublicId { get; set; } = string.Empty;
        // Texto alternativo de la imagen
        public string AltText { get; set; } = string.Empty;
        // Ancho de la imagen
        public int Width { get; set; }
        // Alto de la imagen
        public int Height { get; set; }
        // Formato de la imagen
        public string Format { get; set; } = string.Empty;
        // Tamaño en bytes de la imagen
        public long Bytes { get; set; }
    }
}