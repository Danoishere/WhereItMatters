using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhereItMatters.DataAccess;
using WhereItMatters.Core;
using System.Data.Entity;

namespace WhereItMatters.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly DonationRequestRepository _donationRequestRepository;

        public HomeController(IRepository<Donation> donationRepository, DonationRequestRepository donationRequestRepository)
        {
            _donationRepository = donationRepository;
            _donationRequestRepository = donationRequestRepository;
        }

        public async Task<IActionResult> Index()
        {
            var urgent = await _donationRequestRepository.GetAll()
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.Donations)
                .Take(3).ToListAsync();

            var trending = await _donationRequestRepository.GetAll()
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.Donations)
                .Take(3).ToListAsync();

            ViewData["MostUrgentRequests"] = urgent;
            ViewData["TrendingRequests"] = trending;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
