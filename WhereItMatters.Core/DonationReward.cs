using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class DonationReward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MinimalDonation { get; set; }
        public int DonationRequestId { get; set; }
        public DonationRequest DonationRequest { get; set; }
    }
}
