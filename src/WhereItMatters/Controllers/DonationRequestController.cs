using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Controllers
{
    public class DonationRequestController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public DonationRequestController(IRepository<Donation> donationRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
        }

        public async Task<IActionResult> AllDonationRequests()
        {
            var donationRequests = await _donationRequestRepository.GetAll().ToListAsync();
            return View(donationRequests);
        }

        public IActionResult EditDonationRequest()
        {
            return View();
        }

        public IActionResult ListUrgent()
        {
            return View("ListDonationRequests");
        }

        public IActionResult ListAll()
        {
            return View("ListDonationRequests");
        }

        public async Task<IActionResult> Overview(int id)
        {
            var request = await _donationRequestRepository.GetFullById(id);
            return View(request);
        }
    }
}
