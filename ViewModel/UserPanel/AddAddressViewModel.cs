using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AddAddressViewModel
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("گیرنده")]
        [MaxLength(50)]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("شماره تلفن همراه")]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("آدرس")]
        [MaxLength(300)]
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
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی معتبر نیست.")]
        public string IdentificationCode { get; set; }
     
        [DisplayName("استان")]
         [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string State { get; set; }

       [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Cities { get; set; }
    }
}
