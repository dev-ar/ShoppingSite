using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RegisterViewModel
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string UserName { get; set; }

        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد.")]
        public string Email { get; set; }


        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("تأیید رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمزهای عبور یکسان نیست.")]
        public string PasswordConfirmation { get; set; }
    }

    public class LoginViewModel
    {
        [DisplayName("ایمیل")]
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد.")]
        public string Email { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "رمز عبور شما باید بین ۶ تا ۲۰ کاراکتر باشد.")]
        public string Password { get; set; }

        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [DisplayName("ایمیل")]
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد.")]
        public string Email { get; set; }
    }

    public class RecoveryPasswordViewModel
    {
        [DisplayName("رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("تأیید رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمزهای عبور یکسان نیست.")]
        public string PasswordConfirmation { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [DisplayName("رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "رمز عبور شما باید بین ۸ تا ۲۰ کاراکتر باشد.")]
        public string OldPassword { get; set; }


        [DisplayName("رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "رمز عبور شما باید بیشتر از کاراکتر باشد.")]
        public string Password { get; set; }

        [DisplayName("تأیید رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمزهای عبور یکسان نیست.")]
        [StringLength( 20, MinimumLength = 8, ErrorMessage = "رمز عبور شما باید بیشتر از کاراکتر باشد.")]
        public string PasswordConfirmation { get; set; }
    }
}
