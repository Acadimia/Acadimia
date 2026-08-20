using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Api.Helper.Files
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folderName);
        Task<string> SaveImageWithReducedQuality(IFormFile file, string folderName, int imageQuality, string oldFileName);
        Task<string> SaveFile(string file, string folderName, string extension);
        Task<string> SaveFile(byte[] file, string folderName, string extension);
        Task<byte[]> GetFile(string folderName, string fileName);
        Task DeleteFile(string folderName, string fileName);
    }
}
