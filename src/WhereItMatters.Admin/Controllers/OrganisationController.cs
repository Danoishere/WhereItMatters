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
            var organisations = await  _organisationRepository.GetAll().ToListAsync();
            return View(organisations);
        }

        [Route("{organisationId}")]
        public async Task<IActionResult> Detail(int organisationId)
        {
            var organisation = await _organisationRepository.GetById(organisationId);
            return View(organisation);
        }
    }
}
