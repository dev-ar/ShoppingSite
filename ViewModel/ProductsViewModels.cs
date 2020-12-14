using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class AddCommentViewModel
    {
        public int ProductId { get; set; }

        public int? ParentId { get; set; }

        [DisplayName("متن دیدگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(700)]
        public string Text { get; set; }
    }
}
