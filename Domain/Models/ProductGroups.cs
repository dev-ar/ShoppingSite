﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductGroups
    {
        [Key]
        public int GroupId { get; set; }

        [DisplayName("عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وادر کنید.")]
        [MaxLength(400)]
        public string GroupTitle { get; set; }
        public int? ParentId { get; set; }

        public ProductGroups()
        {
            ProductGroups1 = new HashSet<ProductGroups>();
            SelectedProductGroups = new List<SelectedProductGroup>();
        }
        public virtual ICollection<ProductGroups> ProductGroups1 { get; set; }
        public virtual ProductGroups ProductGroups2 { get; set; }
        public virtual ICollection<SelectedProductGroup> SelectedProductGroups { get; set; }

    }
}
