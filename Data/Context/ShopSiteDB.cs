using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data.Context
{
    public class ShopSiteDB : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<ProductGalleries> ProductGalleries { get; set; }
        public DbSet<ProductTags> ProductTags { get; set; }
        public DbSet<ProductGroups> ProductGroups { get; set; }
        public SelectedProductGroup SelectedProductGroup { get; set; }
        public DbSet<SiteVisit> SiteVisits { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
