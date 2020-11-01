using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Addresses
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("گیرنده")]
        [MaxLength(50)]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("شماره تلفن همراه")]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("استان و شهر")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("آدرس")]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("پلاک")]
        [MaxLength(10)]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("کد پستی")]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("کد ملی")]
        [MaxLength(11)]
        public string IdentificationCode { get; set; }

        public Addresses()
        {
            this.Orders = new HashSet<Orders>();
        }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Users Users { get; set; }
    }
}
