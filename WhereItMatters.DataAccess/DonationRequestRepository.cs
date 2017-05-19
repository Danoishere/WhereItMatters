using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereItMatters.Core;

namespace WhereItMatters.DataAccess
{
    public class DonationRequestRepository : BaseRepository<DonationRequest>
    {
        public DonationRequestRepository(DbContext dbContext) : base(dbContext) { }

        public async Task<DonationRequest> GetFullById(int id)
        {
            var request = await _dbContext.Set<DonationRequest>()
                .Include(r => r.Donations)
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.DonationRewards)
                .FirstOrDefaultAsync(r => r.Id == id);

            return request;
        }

        public async Task<IList<DonationRequest>> GetUrgentReuqests(int number)
        {
            var requests = GetAll();
            requests = requests
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.Donations);

            var requestList = await requests.ToListAsync();
            requestList = requestList.Where(r => !r.IsFinished && !r.IsFinanced).ToList();
            return requestList.OrderByDescending(r => r.ProjectIntensity).Take(number).ToList();
        }

        public async Task<IList<DonationRequest>> GetPopularRequests(int number)
        {
            var requests = GetAll();
            requests = requests
                .Include(r => r.Mission)
                .Include(r => r.Mission.Organisation)
                .Include(r => r.Donations);

            var requestList = await requests.ToListAsync();
            requestList = requestList.Where(r => !r.IsFinished && !r.IsFinanced).ToList();
            return requestList.OrderByDescending(r => r.Donations.Count).Take(number).ToList();
        }
    }
}
