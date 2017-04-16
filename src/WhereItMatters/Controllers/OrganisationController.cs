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
    public class OrganisationController : Controller
    {
        private readonly IRepository<Organisation> _organisationRepository;
        public OrganisationController(IRepository<Organisation> organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }

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
    }
}
