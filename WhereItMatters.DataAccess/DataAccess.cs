using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;

namespace WhereItMatters.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Server=tcp:danomad.database.windows.net,1433;Initial Catalog=WhereItMatters_DEV;Persist Security Info=False;User ID=whereitmatters;Password=Galapagos2016;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
           
        }

        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationRequest> DonationRequests { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<PageInfo> PageInfos { get; set; }
        public DbSet<DonationReward> DonationRewards { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<LocalizedText> LocalizedTexts { get; set; }
    }
}