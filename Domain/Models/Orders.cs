using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [DisplayName("تاریخ سفارش")]
        public DateTime OrderDate { get; set; }

        [Required]
        public bool IsFinal { get; set; }

        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Users Users { get; set; }    
    }
}
