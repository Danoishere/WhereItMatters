using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Admin.Services;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    public class PublicAdminController : Controller
    {

        private readonly IRepository<Organisation> _organisationRepository;
        private readonly IEmailSender _emailService;

        public PublicAdminController(IRepository<Organisation> organisationRepository, IEmailSender emailService)
        {
            _organisationRepository = organisationRepository;
            _emailService = emailService;
        }

        public IActionResult EnlistNGO()
        {
            return View();
        }

        public async Task<IActionResult> SendEnlistmentRequest(Organisation request)
        {
            request.IsActive = false;
            request.IsApproved = false;

            await _organisationRepository.Insert(request);
            await _emailService.SendEmailAsync("", "", "");

            return View("RequestSent");
        }
    }
}
