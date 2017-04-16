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

        public MissionController(IRepository<Mission> missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public async Task<IActionResult> Detail(int missionId)
        {
            var mission = await _missionRepository
                .SearchFor(m => m.Id == missionId)
                .Include(m => m.Requests)
                .FirstOrDefaultAsync();

            return View(mission);
        }
    }
}
