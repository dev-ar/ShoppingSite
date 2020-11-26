using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel
{
    public class AddProductGroupsViewModel
    {
        [DisplayName("عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(60)]
        public string GroupTitle { get; set; }
    }

    public class AddProductsViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300)]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(500)]
        [DisplayName("توضیح مختصر")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("توضیحات")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("قیمت")]
        public int Price { get; set; }

  
        [DisplayName("کلمات کلیدی")]
        [StringLength(50,MinimumLength = 5, ErrorMessage = "لطفا بین ۵ تا ۵۰ کاراکتذ وارد کنید")]
        public string Tags { get; set; }
    }

    public class AddGalleryViewModel
    {

        [DisplayName("عنوان")]
        [MaxLength(250)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string GalleryTitle { get; set; }

     
    }
}
