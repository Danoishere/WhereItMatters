﻿using System;
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
        public double AmountUSD { get; set; }
        public bool ShowInDonationLog { get; set; }
        public DateTime TimeStamp { get; set; }

        public int DonationRequestId { get; set; }
        public DonationRequest DonationRequest { get; set; }
    }
}
