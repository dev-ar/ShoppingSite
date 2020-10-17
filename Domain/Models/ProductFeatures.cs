using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductFeatures
    {
        [Key]
        public int ProductFeatureId { get; set; }


        [DisplayName("کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int ProductId { get; set; }

        [DisplayName("ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int FeatureId { get; set; }

        [DisplayName("مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Value { get; set; }

        public virtual Features Features { get; set; }
        public virtual Products Products { get; set; }  

    }
}
