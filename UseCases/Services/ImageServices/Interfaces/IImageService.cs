using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UseCases.Services.ImageServices.Interfaces

{
    public interface IImageService
    {
        public Task<string> UploadImageAsync(IFormFile file);
        public Task<string?> ReplaceImageAsync(string name, IFormFile? newImage);
    }
}
