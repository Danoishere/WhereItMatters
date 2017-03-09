using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    [Table("PageInfos")]
    public class PageInfo
    {
        [Key]
        public string Key { get; set; }
        public string Content { get; set; }
    }
}
