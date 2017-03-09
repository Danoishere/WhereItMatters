using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;
using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhereItMatters.Controllers
{
    [Route("donationrequest/api/donationdata")]
    public class DonationDataAPIController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public DonationDataAPIController(IRepository<Donation> donationRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
        }

        [HttpGet("requests")]
        public async Task<IEnumerable<DonationRequest>> GetRequests(string searchTerms = "", bool[] priority = null)
        {


            var requests = _donationRequestRepository.GetAll().Include(r => r.Donations);
            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                foreach (var term in searchTerms.Split(' '))
                {
                    requests = requests.Where(r => r.Title.Contains(term) || r.ShortSummary.Contains(term) || r.Description.Contains(term));
                }
            }

            if(priority != null && priority.Any(p => p == true))
            {
                var list = new List<DonationRequest>();

                for (int i = 0; i < priority.Length; i++)
                {
                    var currentPriority = (Priority)i;

                    if (priority[i] == false)
                    {
                        requests = requests.Where(r => r.Priority != currentPriority);
                    }
                }
            }
                
            return await requests.ToListAsync();
        }

        [HttpGet("requests/{id}")]
        public async Task<DonationRequest> GetRequest(int id)
        {
            return await _donationRequestRepository.GetFullById(id);
        }
    }
}
