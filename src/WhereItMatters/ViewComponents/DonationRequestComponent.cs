using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.ViewComponents
{
    [ViewComponent(Name = "DonationRequest")]
    public class DonationRequestComponent : ViewComponent
    {
        private readonly DonationRequestRepository _repository;
        public DonationRequestComponent(DonationRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int donationRequestId, DonationRequest request)
        {
            if(request == null)
            {
                request = await _repository.GetById(donationRequestId);
            }

            return View(request);
        }
    }
}
