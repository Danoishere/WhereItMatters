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
    public class OrganisationController : Controller
    {

        private readonly IRepository<Organisation> _organisationRepository;

        public OrganisationController(IRepository<Organisation> organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }


        [Authorize(Roles = AppConfig.RoleADMIN)]
        public async Task<IActionResult> List()
        {
            var organisations = await _organisationRepository.GetAll().ToListAsync();
            return View(organisations);
        }

        public async Task<IActionResult> Detail(int organisationId)
        {
            var organisation = await _organisationRepository
                .SearchFor(o => o.Id == organisationId)
                .Include(o => o.Locations)
                .FirstOrDefaultAsync();

            return View(organisation);
        }

        [Route("Edit/{orginasitonId}")]
        public async Task<IActionResult> Edit(int organisationId)
        {
            var organisation = await _organisationRepository.GetById(organisationId);
            return View(organisation);
        }

        [HttpPost]
        [Route("Edit/{orginasitonId}")]
        public async Task<IActionResult> Edit(int organisationId, Organisation organisation)
        {
            if (organisationId == 0)
            {
                await _organisationRepository.Insert(organisation);
            }
            else
            {
                await _organisationRepository.Update(organisation);
            }
            return View(organisation);
        }
    }
}
