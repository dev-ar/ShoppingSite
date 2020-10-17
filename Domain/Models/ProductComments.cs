using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductComments
    {
        [Key]
        public int Commentid { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100)]
        [DisplayName("نام کاربری")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("ایمیل")]
        [MaxLength(150)]
        public string Email { get; set; }

        [DisplayName("سایت")]
        [MaxLength(150)]
        public string WebSite { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(800)]
        [DisplayName("نظر")]
        public string Text { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("تاریخ ثبت نظر")]
        public DateTime CreateDate { get; set; }
        public int? ParentId { get; set; }

        public ProductComments()
        {
            ProductComments1 = new HashSet<ProductComments>();
        }

        public virtual ICollection<ProductComments> ProductComments1 { get; set; }
        public virtual ProductComments ProductComments2 { get; set; }
        public virtual Products Products { get; set; }  

    }
}
