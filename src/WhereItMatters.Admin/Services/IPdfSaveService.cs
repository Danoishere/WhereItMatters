using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.Admin.Services
{
    public interface IPdfSaveService
    {
        Task<string> SavePdf(HttpRequest request);
    }
}
