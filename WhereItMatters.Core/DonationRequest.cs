﻿using System;
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
        public string BudgetPdfUrl { get; set; }
        public string Title { get; set; }
        public string ShortSummary { get; set; }
        public string Description { get; set; }
        public decimal NeededAmountUSD { get; set; }
        public Priority Priority { get; set; }
        public int ApproxPopulationImpacted { get; set; }

        public List<Donation> Donations { get; set; }
        public List<DonationReward> DonationRewards { get; set; }
        public List<BudgetItem> BudgetItems { get; set; }
        public List<GalleryItem> GalleryItems { get; set; }

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

        public string NeededAmountUSDString
        {
            get { return NeededAmountUSD.MoneyFormat(); }
        }

        public decimal PercentageOfFulfillment
        {
            get
            {
                if (Donations == null)
                    return 0;

                return Donations.Sum(d => d.AmountUSD)/NeededAmountUSD;
            }
        }

        public decimal Donated
        {
            get
            {
                if (Donations == null)
                    return 0;

                return Donations.Sum(d => d.AmountUSD);
            }
        }

        public string DonatedString
        {
            get { return Donated.MoneyFormat(); }
        }

        public decimal RemainingUSDNeeded
        {
            get
            {
                if (Donations == null)
                    return NeededAmountUSD;

                return NeededAmountUSD - Donations.Sum(d => d.AmountUSD);
            }
        }

        public string RemainingUSDNeededString
        {
            get { return RemainingUSDNeeded.MoneyFormat(); }
        }

        public string FullImageUrl
        {
            get
            {
                return ImageConfig.Url + ImageUrl;
            }
        }

        public string FullBudgetPdfUrl
        {
            get
            {
                return PdfConfig.Url + BudgetPdfUrl;
            }
        }

        public double ProjectIntensity
        {
            get
            {
                var daysUntilEnd = (double)DaysUntilEnd;
                var priorityDecimal = (double)((int)Priority + 1);
                var populationDecimal = (double)ApproxPopulationImpacted;
                var impactDecimal = populationDecimal/priorityDecimal;

                var top = Math.Pow(daysUntilEnd, -1.0);
                var bottom = Math.Pow(impactDecimal, -1.0);


                return top/bottom;
            }
        }

        public string PriorityString
        {
            get
            {
                return Priority.GetEnumDescription();
            }
        }

        public bool HasImage
        {
            get
            {
                return !string.IsNullOrEmpty(ImageUrl);
            }
        }
    }
}
