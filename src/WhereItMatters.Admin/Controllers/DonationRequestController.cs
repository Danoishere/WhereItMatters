using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Threading.Tasks;
using WhereItMatters.Admin.Services;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    [Route("DonationRequest")]
    public class DonationRequestController : Controller
    {
        private readonly DonationRequestRepository _requestRepository;
        private readonly IRepository<Donation> _donationRepository;
        private readonly IImageSaveService _imageSaveService;

        public DonationRequestController(
            DonationRequestRepository requestRepository, 
            IRepository<Donation> donationRepository,
            IImageSaveService imageSaveService)
        {
            _requestRepository = requestRepository;
            _donationRepository = donationRepository;
            _imageSaveService = imageSaveService;
        }

        [Route("Detail/{requestId}")]
        public async Task<IActionResult> Detail(int requestId)
        {
            var request = await _requestRepository.GetFullById(requestId);

            var emailList = new List<string>();
            foreach(var donation in request.Donations)
            {
                emailList.Add(donation.DonorEmail);
            }
            emailList = emailList.Distinct().ToList();
            ViewData["DonorMailAddresses"] = string.Join(", ", emailList);

            return View(request);
        }

        [HttpGet]
        [Route("Create/{missionId}")]
        public IActionResult Create(int missionId)
        {
            var request = new DonationRequest
            {
                CreatedOn = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                MissionId = missionId,
            };

            return View("Edit", request);
        }

        [HttpGet]
        [Route("Edit/{requestId}")]
        public async Task<IActionResult> Edit(int requestId)
        {
            var request = await _requestRepository.GetFullById(requestId);
            return View(request);
        }

        [HttpPost]
        [Route("Edit/{requestId}")]
        public async Task<IActionResult> Edit(int requestId, DonationRequest request, IList<IFormFile> files)
        {
            var f = files;
            var newImageUrl = await _imageSaveService.SaveImage(Request);
            if (!string.IsNullOrEmpty(newImageUrl))
            {
                request.ImageUrl = newImageUrl;
            }

            if (requestId == 0)
            {
                await _requestRepository.Insert(request);
            }
            else
            {
                request.Donations = await _donationRepository.SearchFor(r => r.DonationRequestId == requestId).ToListAsync();
                await _requestRepository.Update(request);
            }

            return RedirectToAction("Edit", new { requestId = request.Id });
        }
    }
}
