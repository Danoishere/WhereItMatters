using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {

        private readonly IRepository<Mission> _missionRepository;

        public MissionController(IRepository<Mission> missionRepository)
        {
            _missionRepository = missionRepository;
        }


        public async Task<IActionResult> List(int organisationId)
        {
            if(User.IsInRole(AppConfig.RoleADMIN) || User.OrganisationId() == organisationId)
            {
                var missions = await _missionRepository.SearchFor(m => m.OrganisationId == organisationId).ToListAsync();
                return View(missions);
            }

            return View(new List<Mission>());
        }


        public async Task<IActionResult> Detail(int missionId)
        {
            var mission = await _missionRepository.SearchFor(m => m.Id == missionId).Include(m => m.Requests).FirstOrDefaultAsync();
            if (User.IsInRole(AppConfig.RoleADMIN) || User.OrganisationId() == mission.OrganisationId)
            {
                return View(mission);
            }

            return View(new List<Mission>());
        }
    }
}
