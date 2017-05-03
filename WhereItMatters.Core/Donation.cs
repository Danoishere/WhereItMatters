using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class Donation : IEntity
    {
        public int Id { get; set; }

        public string DonorEmail { get; set; }
        public string DonorName { get; set; }
        public string Comment { get; set; }
        public bool DenySupportMoney { get; set; }
        public decimal AmountUSD { get; set; }

        public string AmountUSDString
        {
            get { return AmountUSD.MoneyFormat(); }
        }

        public bool ShowInDonationLog { get; set; }
        public DateTime TimeStamp { get; set; }

        public int? DonationRequestId { get; set; }
        public DonationRequest DonationRequest { get; set; }

        public int? MissionId { get; set; }
        public Mission Mission { get; set; }

        public int? OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
    }
}
