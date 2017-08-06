using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;
using Microsoft.AspNetCore.Http;
using Braintree;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhereItMatters.Controllers
{
    public class DonationController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly IRepository<Mission> _missionRepository;
        private readonly IRepository<Organisation> _organisationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;
        private readonly IBraintreeGateway _gateway;

        public DonationController(
            IRepository<Donation> donationRepository, 
            DonationRequestRepository donationRequestRepository,
            IRepository<Organisation> organisationRepository,
            IRepository<Mission> missionRepository,
            IBraintreeGateway gateway)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
            _missionRepository = missionRepository;
            _organisationRepository = organisationRepository;
            _gateway = gateway;
        }

        public async Task<IActionResult> DonationDetailsForRequest(int requestId, decimal amount)
        {
            var donation = new Donation
            {
                DonationRequestId = requestId,
            };

            donation.DonationRequest = await _donationRequestRepository.GetFullById(requestId);

            // Clamp donation amount to max possible and 0
            amount = amount.Clamp(0, donation.DonationRequest.RemainingUSDNeeded);
            donation.AmountUSD = amount;

            ViewData["DonationType"] = DonationType.DonationRequest;
            
            return View("DonationDetails", donation);
        }

        public async Task<IActionResult> DonationDetailsForMission(int missionId, decimal amount)
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

        public async Task<IActionResult> DonationDetailsForOrganisation(int organisationId, decimal amount)
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
        public async Task<IActionResult> DonationPaymentForm(Donation donation)
        {
            string donationFor = "";
            if (donation.DonationRequestId.HasValue)
            {
                donation.DonationRequest = await _donationRequestRepository.GetFullById(donation.DonationRequestId.Value);
                donationFor = "Donation for " + donation.DonationRequest.Title;
            }
            else if (donation.MissionId.HasValue)
            {
                donation.Mission = await _missionRepository.GetById(donation.MissionId.Value);
                donationFor = "Donation for the mission " + donation.Mission.Name;
            }
            else if (donation.OrganisationId.HasValue)
            {
                donation.Organisation = await _organisationRepository.GetById(donation.OrganisationId.Value);
                donationFor = "Donation towards " + donation.Organisation.Name;
            }

            ViewData["BraintreeAuthorizationToken"] = await _gateway.ClientToken.generateAsync();
            ViewData["DonationPurpose"] = donationFor;

            return View(donation);
        }

        [HttpPost]
        public async Task<ActionResult> ExecuteDonationPayment(IFormCollection collection, Donation donation)
        {
            var nonceFromTheClient = collection["payment_method_nonce"];
            var cardholderFirstname = collection["cardholder_firstname"];
            var cardholderLastname = collection["cardholder_lastname"];

            var request = new TransactionRequest
            {
                Amount = (decimal)donation.AmountUSD,
                PaymentMethodNonce = nonceFromTheClient,
                BillingAddress = new AddressRequest 
                {
                    FirstName = cardholderFirstname,
                    LastName = cardholderLastname,
                },
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var result = await _gateway.Transaction.SaleAsync(request);
            if (result.IsSuccess())
            {
                donation.TimeStamp = DateTime.Now;
                await _donationRepository.Insert(donation);
            }

            return View("DonationPaymentResult", result);
        }
    }
}
