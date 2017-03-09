using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class Mission : IEntity
    {
        public int Id { get; set; }

        public Mission() { }

        public Mission(string name, double lon,double lat)
        {
            Name = name;
            Longitude = lon;
            Latitude = lat;
        }

        public string Name { get; set; }
        public string Comment { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }

        public List<DonationRequest> Requests { get; set; }
    }
}
