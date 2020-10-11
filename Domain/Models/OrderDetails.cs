using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDetails
    {
        [Key]
        public int DetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وادر کنید.")]
        [DisplayName("قیمت")]
        public int Price { get; set; }

        [Required]
        [DisplayName("تعداد")]
        public int Count { get; set; }


        public virtual Products Products { get; set; }  
        public virtual Orders Orders { get; set; }
    }
}
