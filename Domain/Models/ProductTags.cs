using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductTags
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [DisplayName("کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وادر کنید.")]
        [MaxLength(250)]
        public string Tag { get; set; }

        public virtual Products Products { get; set; }
    }
}
