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
        public int CommentId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; } 

        [DisplayName("عنوان نظر")]
        [MaxLength(150)]
        public string CommentTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(800)]
        [DisplayName("نظر")]
        public string Text { get; set; }

        [Range(1,5)]
        [DisplayName("امتیاز")]
        public decimal Rate { get; set; }   

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
        public virtual Users Users { get; set; }

    }
}
