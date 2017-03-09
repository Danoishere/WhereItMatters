using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.ViewComponents
{
    [ViewComponent(Name = "DonationRequestList")]
    public class DonationRequestListComponent : ViewComponent
    {
        private readonly DonationRequestRepository _repository;
        public DonationRequestListComponent(DonationRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _repository.GetAll()
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.Donations).ToListAsync();
            return View(items);
        }
    }
}
