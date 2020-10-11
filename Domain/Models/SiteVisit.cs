using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SiteVisit
    {
        [Key]
        public int VisitId { get; set; }

        [Required]
        public string IP { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }
    }
}
