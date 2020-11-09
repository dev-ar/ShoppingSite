using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cities
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CityId { get; set; }

        [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50)]
        public string CityTitle { get; set; }

        [Required]
        public int StateId { get; set; }

        public States States { get; set; }
    }
}
