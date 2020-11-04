using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int RoleId { get; set; }

        [MaxLength(200)]
        [DisplayName("نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserName { get; set; }

        [MaxLength(200)]
        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Password { get; set; }

        [MaxLength(200)]
        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Email { get; set; }

        [DisplayName("کد فعالسازی")]
        [MaxLength(200)]
        [Required]
        public string ActiveCode { get; set; }

        [Required]
        [DisplayName("فعال")]
        public bool IsActive { get; set; }

        [DisplayName("تاریخ ثبت‌نام")]
        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastLoginIp { get; set; }

        [Required]
        [DisplayName("تاریخ آخرین بازدید")]
        public DateTime LastLoginDate { get; set; }


        public Users()
        {
            this.Orders = new HashSet<Orders>();
            this.Addresses = new HashSet<Addresses>();
        }

        public virtual ICollection<Addresses> Addresses  { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Roles Roles { get; set; }    
    }
}
