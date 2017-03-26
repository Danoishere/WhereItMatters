using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class DonationRequest : IEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string ShortSummary { get; set; }
        public string Description { get; set; }
        public double NeededAmountUSD { get; set; }
        public Priority Priority { get; set; }
        public int ApproxPopulationImpacted { get; set; }
        public List<Donation> Donations { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public DateTime EndDate { get; set; }

        public int MissionId { get; set; }
        public Mission Mission { get; set; }

        public int DaysUntilEnd
        {
            get { return Math.Max(0,(EndDate - DateTime.Today).Days); }
        }

        public bool IsFinanced
        {
            get
            {
                if (Donations == null)
                    return false;

                return NeededAmountUSD == Donations.Sum(d => d.AmountUSD);
            }
        }

        public bool IsFinished
        {
            get { return (EndDate - DateTime.Today).Days < 0; }
        }

        public double PercentageOfFulfillment
        {
            get
            {
                if (Donations == null)
                    return 0;

                return Donations.Sum(d => d.AmountUSD)/NeededAmountUSD;
            }
        }

        public double Donated
        {
            get
            {
                if (Donations == null)
                    return 0;

                return Donations.Sum(d => d.AmountUSD);
            }
        }

        public double RemainingUSDNeeded
        {
            get
            {
                if (Donations == null)
                    return NeededAmountUSD;

                return NeededAmountUSD - Donations.Sum(d => d.AmountUSD);
            }
        }

        public string FullImageUrl
        {
            get
            {
                return ImageConfig.Url + ImageUrl;
            }
        }
    }
}
