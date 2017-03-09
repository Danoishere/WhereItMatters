using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.DataAccess;
using WhereItMatters.Core;

namespace WhereItMatters.Controllers
{
    public class DonationRequestAdminController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public DonationRequestAdminController(IRepository<Donation> donationRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
        }

        public async Task<IActionResult> EditDonationRequest(int id)
        {
            DonationRequest request;
            if(id == 0)
            {
                request = new DonationRequest();
            }
            else
            {
                request = await _donationRequestRepository.GetFullById(id);
            }
            return View(request);
        }

        public async Task<IActionResult> SaveDonationRequest(DonationRequest request)
        {
            if(request.Id == 0)
            {
                await _donationRequestRepository.Insert(request);
            }
            else
            {
                await _donationRequestRepository.Update(request);
            }

            return RedirectToAction("EditDonationRequest", new { id = request.Id });
        }
    }
}
