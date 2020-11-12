using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductGalleries
    {
        [Key]
        public int GalleryId { get; set; }

        [DisplayName("کالا")]
        [Required]
        public int ProductId { get; set; }

        [DisplayName("عنوان")]
        [MaxLength(250)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string GalleryTitle { get; set; }

        [Required]
        [DisplayName("تصویر")]
        [MaxLength(50)]
        public string ImageName { get; set; }


        public virtual Products Products { get; set; }  
    }
}
