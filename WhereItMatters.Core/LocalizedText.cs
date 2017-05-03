using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class LocalizedText 
    {
        [Key]
        public string Identifier { get; set; }
        public string ValueEN { get; set; }
    }
}
