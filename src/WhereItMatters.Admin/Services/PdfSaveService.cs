using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

namespace WhereItMatters.Admin.Services
{
    public class PdfSaveService : IPdfSaveService
    {
        private readonly IHostingEnvironment _environment;
        public PdfSaveService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SavePdf(HttpRequest request)
        {
            var files = request.Form.Files;
            foreach (var pdf in files.Where(f => f.FileName.ToLower().EndsWith("pdf")))
            {
                if (pdf != null && pdf.Length > 0)
                {
                    var file = pdf;
                    var uploads = Path.Combine(_environment.WebRootPath, "pdfs");

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
