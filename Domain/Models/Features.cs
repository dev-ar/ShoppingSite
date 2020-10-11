using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Features
    {
        [Key]
        public int FeatureId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وادر کنید.")]
        [DisplayName("ویژگی‌ها")]
        [MaxLength(200)]
        public string FeatureTitle { get; set; }

        public Features()
        {
            ProductFeatures = new HashSet<ProductFeatures>();
        }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
    }
}
