using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.Formaters.Interfaces;
using UseCases.Services.ImageServices.Interfaces;

namespace UseCases.Services.Formaters
{
    public class ImageFormater(IImageService imageService) : IImageFormater
    {
        string IImageFormater.Format(string fileName)
        {
            return imageService.GetImageLink(fileName); 
        }
    }
}
