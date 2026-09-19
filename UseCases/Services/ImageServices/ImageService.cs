using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Internal;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UseCases.Services.ImageServices.Interfaces;

namespace UseCases.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly AmazonS3Client client;
        private readonly string bucketName;

        public ImageService(IConfiguration configuration)
        {
            var options = configuration.GetSection("ImageServices:IDriveE2");

            var config = new AmazonS3Config
            {
                ServiceURL = options["ServiceUrl"],
                ForcePathStyle = true
            };

            client = new AmazonS3Client(options["AccessKey"], options["SecretKey"], config);
            bucketName = options["BucketName"];
        }
        async Task<string?> IImageService.ReplaceImageAsync(string name, IFormFile? newImage)
        {
            await client.DeleteObjectAsync(bucketName, name);
            return await ((IImageService)this).UploadImageAsync(newImage);
        }

        async Task<string> IImageService.UploadImageAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = file.OpenReadStream();

            var uploadRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = stream,
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await client.PutObjectAsync(uploadRequest);

            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileName,
                Expires = DateTime.UtcNow.AddMinutes(10000),
                Verb = HttpVerb.GET
            };

            return client.GetPreSignedURL(request);
        }
    }
}
