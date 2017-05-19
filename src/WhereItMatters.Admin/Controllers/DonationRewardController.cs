using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    public class DonationRewardController : Controller
    {
        private readonly IRepository<DonationReward> _donationRewardRepository;

        public DonationRewardController(IRepository<DonationReward> donationRewardRepository)
        {
            _donationRewardRepository = donationRewardRepository;
        }

        public IActionResult Create(int donationRequestId)
        {
            var donationReward = new DonationReward
            {
                DonationRequestId = donationRequestId,
            };
            return View("Edit", donationReward);
        }

        public async Task<IActionResult> Edit(int donationRewardId)
        {
            var donationReward = await _donationRewardRepository.GetById(donationRewardId);
            return View(donationReward);
        }

        public async Task<IActionResult> Save(DonationReward donationReward)
        {
            if(donationReward.Id == 0)
            {
                await _donationRewardRepository.Insert(donationReward);
            }
            else
            {
                await _donationRewardRepository.Update(donationReward);
            }
            
            return View("Edit", donationReward);
        }
    }
}
