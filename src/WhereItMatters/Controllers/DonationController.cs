using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhereItMatters.Controllers
{
    public class DonationController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public DonationController(IRepository<Donation> donationRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
        }

        public async Task<IActionResult> DonationDetails(int requestId, double amount)
        {
            var donation = new Donation
            {
                DonationRequestId = requestId,
                AmountUSD = amount,
            };

            donation.DonationRequest = await _donationRequestRepository.GetFullById(requestId);

            return View(donation);
        }

        [HttpPost]
        public async Task<IActionResult> DonationPayment(Donation donation)
        {
            // proceed payment
            await Task.Delay(0);

            return View(donation);
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteDonationPayment(Donation donation)
        {
            donation.TimeStamp = DateTime.Now;
            await _donationRepository.Insert(donation);
            return View("DonationPaymentResult");
        }
    }
}
