using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;
using Microsoft.AspNetCore.Identity;
using WhereItMatters.Admin.Models;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {
        private readonly IRepository<Mission> _missionRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MissionController(IRepository<Mission> missionRepository, UserManager<ApplicationUser> userManager)
        {
            _missionRepository = missionRepository;
            _userManager = userManager;
        }


        public async Task<IActionResult> List(int organisationId)
        {

            var ngoUserOrganisationId = await _userManager.GetOrganisationId(HttpContext);

            if (User.IsInRole(AppConfig.RoleADMIN) || (ngoUserOrganisationId.HasValue && ngoUserOrganisationId.Value == organisationId))
            {
                var missions = await _missionRepository.SearchFor(m => m.OrganisationId == organisationId).ToListAsync();
                return View(missions);
            }

            return View(new List<Mission>());
        }


        public async Task<IActionResult> Detail(int missionId)
        {
            var ngoUserOrganisationId = await _userManager.GetOrganisationId(HttpContext);
            var mission = await _missionRepository.SearchFor(m => m.Id == missionId).Include(m => m.Requests).FirstOrDefaultAsync();
            if (User.IsInRole(AppConfig.RoleADMIN) || (ngoUserOrganisationId.HasValue && ngoUserOrganisationId.Value == mission.OrganisationId))
            {
                return View(mission);
            }

            return View(new List<Mission>());
        }

        public async Task<IActionResult> Edit(int missionId)
        {
            if (missionId != 0)
            {
                var mission = await _missionRepository.GetById(missionId);
                return View(mission);
            }

            return View(new Mission());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int missionId, Mission mission)
        {
            if (missionId == 0)
            {
                await _missionRepository.Insert(mission);
            }
            else
            {
                await _missionRepository.Update(mission);
            }

            return View(mission);
        }
    }
}