﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

namespace WhereItMatters.Admin.Services
{
    public class ImageSaveService : IImageSaveService
    {
        private readonly IHostingEnvironment _environment;
        public ImageSaveService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImage(HttpRequest request)
        {
            var files = request.Form.Files;
            foreach (var image in files.Where(f => f.FileName.ToLower().EndsWith("jpg") || f.FileName.ToLower().EndsWith("jpeg") || f.FileName.ToLower().EndsWith("png")))
            {
                if (image != null && image.Length > 0)
                {
                    var file = image;
                    var uploads = Path.Combine(_environment.WebRootPath, "images");

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            return file.FileName;
                        }
                    }
                }
            }

            return null;
        }
    }
}
