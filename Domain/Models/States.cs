using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Domain
{
    public class States
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }
        [DisplayName("استان")]
         [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50)]
        public string StateTitle { get; set; }

        public States()
        {
            Cities = new HashSet<Cities>();
        }
        public ICollection<Cities> Cities { get; set; }
    }
}
