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
        public DonationRequestRepository(DbContext dbContext) : base(dbContext){}

        public async Task<DonationRequest> GetFullById(int id)
        {
            var request = await _dbContext.Set<DonationRequest>().Include(r => r.Donations).FirstOrDefaultAsync(r => r.Id == id);
            return request;
        }
    }
}
