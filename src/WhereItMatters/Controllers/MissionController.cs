using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.DataAccess;
using WhereItMatters.Core;
using System.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhereItMatters.Controllers
{
    public class MissionController : Controller
    {
        private readonly IRepository<Mission> _missionRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public MissionController(IRepository<Mission> missionRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRequestRepository = donationRequestRepository;
            _missionRepository = missionRepository;
        }

        public async Task<IActionResult> Detail(int missionId)
        {
            var mission = await _missionRepository.GetById(missionId);
            mission.Requests = await _donationRequestRepository
                .SearchFor(d => d.MissionId == missionId)
                .Include(d => d.Donations)
                .ToListAsync();

            return View(mission);
        }
    }
}
