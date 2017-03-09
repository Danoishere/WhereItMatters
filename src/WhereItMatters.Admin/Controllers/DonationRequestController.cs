using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    public class DonationRequestController : Controller
    {
        private readonly DonationRequestRepository _requestRepository;
        public DonationRequestController(DonationRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }


        public async Task<IActionResult> Detail(int requestId)
        {
            var request = await _requestRepository.GetFullById(requestId);
            return View(request);
        }
    }
}
