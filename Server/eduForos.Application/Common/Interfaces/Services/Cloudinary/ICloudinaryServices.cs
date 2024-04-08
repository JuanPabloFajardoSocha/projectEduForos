using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;

namespace eduForos.Application.Common.Interfaces.Services.Cloudinary;

    public interface ICloudinaryService
    {
        Task<CloudinaryResponse?> UploadImageAsync(IFormFile file, string directory);
        Task<bool> CheckedFileAsync(string assetId);
        Task<bool> DeleteImageAsync(string assetId);
        
    }