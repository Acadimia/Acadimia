using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Acadimia.Infrastructure.Services;
using Acadimia.Data.Resources;

namespace Acadimia.Api.Helper.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                var filePath = Path.Combine(uploads, fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        public async Task<string> SaveImageWithReducedQuality(IFormFile file, string folderName, int imageQuality, string oldFileName = null)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                var filePath = Path.Combine(uploads, fileName);

                // Save the uploaded file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Reduce the image quality using Magick.NET
                using (var image = new MagickImage(filePath))
                {
                    image.Quality = (uint)imageQuality;
                    image.Write(filePath);
                }
            }
            return fileName;
        }

        public async Task<string> SaveFile(byte[] file, string folderName, string extension)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
            }
            return fileName;
        }

        public async Task<string> SaveFile(string file, string folderName, string extension)
        {
            string fileName = null;
            if (!string.IsNullOrWhiteSpace(file))
            {
                var bytes = Convert.FromBase64String(file);
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
            }
            return fileName;
        }

        public async Task DeleteFile(string folderName, string fileName = null)
        {
            var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
