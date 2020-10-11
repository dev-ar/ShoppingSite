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
    public class Roles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }

        [MaxLength(200)]
        [DisplayName("نام لاتین نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string RoleName { get; set; }
        
        
        [MaxLength(200)]
        [DisplayName("عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string RoleTitle { get; set; }

        public Roles()
        {
            Users = new HashSet<Users>();
        }
        public virtual ICollection<Users> Users { get; set; }
    }
}
