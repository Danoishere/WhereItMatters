using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    [Authorize]
    public class LocalizationController : Controller
    {
        private readonly LocalizationRepository _localizationRepository;
        public LocalizationController(LocalizationRepository localizationRepository)
        {
            _localizationRepository = localizationRepository;
        }

        public async Task<IActionResult> List()
        {
            var localizations = await _localizationRepository.GetAll().ToListAsync();
            return View(localizations);
        }

        public async Task<IActionResult> SaveLocalization(string identifier, string valueEn)
        {
            var localization = await _localizationRepository.SearchFor(l => l.Identifier == identifier).FirstOrDefaultAsync();
            if(localization != null)
            {
                localization.ValueEN = valueEn;
                await _localizationRepository.Update(localization);
            }

            return RedirectToAction("List");
        }
    }
}
