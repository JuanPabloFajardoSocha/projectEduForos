using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace eduForos.Infrastructure.Services.CloudinaryServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration)
        {
            Account account = new(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> CheckedFileAsync(string assetId)
        {
            var result = await _cloudinary.GetResourceAsync(assetId);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<CloudinaryResponse?> UploadImageAsync(IFormFile file, string directory)
        {
            try
            {
                var uploadResult = new ImageUploadResult();
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = directory
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }

                var Cloudinary = new CloudinaryResponse(
                    uploadResult.PublicId,
                    uploadResult.SecureUrl.ToString()
                    );

                return Cloudinary;
            }
            catch (Exception e)
            {
                throw new Exception("Error al subir la imagen: " + e.Message);
            }
        }

        public async Task<bool> DeleteImageAsync(string assetId)
        {
            var deleteParams = new DeletionParams(assetId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }



        public void ViewImageAsync(string url) // Por ahora no se usa.. Falta probarlo
        {
            try
            {
                Cloudinary cloudinary = new Cloudinary();

                GetResourceResult result = cloudinary.GetResource(url);

                if (result != null)
                {
                    Console.WriteLine(result.Url);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al visualizar la imagen: " + e.Message);
            }
        }

        public void DeleteImage(string url) // Por ahora no se usa.. Falta probarlo
        {
            try
            {
                var publicId = url.Split("/").Last().Split(".").First();
                var deleteParams = new DeletionParams(publicId);
                var result = _cloudinary.Destroy(deleteParams);
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar la imagen: " + e.Message);
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            try
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                };
                var result = await _cloudinary.UploadAsync(uploadParams);
                return result.Url.ToString();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error al subir el video: " + e.Message);
            }

        }
    }
}