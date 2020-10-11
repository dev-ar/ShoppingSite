using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SelectedProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public virtual ProductGroups ProductGroups { get; set; }
        public virtual Products Products { get; set; }  
    }
}
