using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300)]
        public string SliderTitle { get; set; }

        [DisplayName("تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string ImageName { get; set; }

        [DisplayName("تاریخ شروع")]
        [Required]
        [DisplayFormat(DataFormatString = "0: yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("تاریخ پایان")]
        [Required]
        [DisplayFormat(DataFormatString = "0: yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayName("وضعیت")]
        [Required]
        public bool IsActive { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Url(ErrorMessage = "آدرس وارد شده معتبر نمی‌باشد.")]
        [MaxLength(450)]
        public string Url { get; set; }
    }
}
