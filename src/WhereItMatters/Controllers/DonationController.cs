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
        private readonly IRepository<Mission> _missionRepository;
        private readonly IRepository<Organisation> _organisationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public DonationController(
            IRepository<Donation> donationRepository, 
            DonationRequestRepository donationRequestRepository,
            IRepository<Organisation> organisationRepository,
            IRepository<Mission> missionRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
            _missionRepository = missionRepository;
            _organisationRepository = organisationRepository;
        }

        public async Task<IActionResult> DonationDetailsForRequest(DonationType donationType, int requestId, double amount)
        {
            var donation = new Donation
            {
                DonationRequestId = requestId,
                AmountUSD = amount,
            };

            ViewData["DonationType"] = DonationType.DonationRequest;
            donation.DonationRequest = await _donationRequestRepository.GetFullById(requestId);
            return View("DonationDetails", donation);
        }

        public async Task<IActionResult> DonationDetailsForMission(DonationType donationType, int missionId, double amount)
        {
            var donation = new Donation
            {
                MissionId = missionId,
                AmountUSD = amount,
            };

            ViewData["DonationType"] = DonationType.Mission;
            donation.Mission = await _missionRepository.GetById(missionId);
            return View("DonationDetails", donation);
        }

        public async Task<IActionResult> DonationDetailsForOrganisation(DonationType donationType, int organisationId, double amount)
        {
            var donation = new Donation
            {
                OrganisationId = organisationId,
                AmountUSD = amount,
            };

            ViewData["DonationType"] = DonationType.Organisation;
            donation.Organisation = await _organisationRepository.GetById(organisationId);
            return View("DonationDetails", donation);
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
