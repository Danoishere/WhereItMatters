using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class Organisation : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public string Mail { get; set; }
        public List<Mission> Locations { get; set; }

    }
}
