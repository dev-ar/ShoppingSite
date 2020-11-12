﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300)]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(500)]
        [DisplayName("توضیح مختصر")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DisplayName("قیمت")]
        public int Price { get; set; }

        [Required]
        [DisplayName("تصویر")]
        [MaxLength(50)]
        public string ImageName { get; set; }

        [Range(1,5)]
        [DisplayName("امتیاز محصول")]
        [Required]
        public decimal ProductRate { get; set; }    

        [DisplayName("تاریخ ثبت")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        [Required]
        public DateTime CreateDate { get; set; }

        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ProductTags = new HashSet<ProductTags>();
            ProductGalleries = new HashSet<ProductGalleries>();
            ProductFeatures = new HashSet<ProductFeatures>();
            SelectedProductGroups = new HashSet<SelectedProductGroup>();
            ProductComments = new HashSet<ProductComments>();
        }

        public ICollection<ProductComments> ProductComments { get; set; }
        public ICollection<SelectedProductGroup> SelectedProductGroups { get; set; }    
        public ICollection<ProductFeatures> ProductFeatures { get; set; }
        public ICollection<ProductGalleries> ProductGalleries { get; set; }
        public ICollection<ProductTags> ProductTags{ get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }   
    }
}
