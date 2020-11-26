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
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGroups>()
                .HasMany(p => p.ProductGroups1)
                .WithRequired(child => child.ProductGroups2)
                .HasForeignKey(child => child.ParentId);

            modelBuilder.Entity<ProductComments>()
                .HasMany(p => p.ProductComments1)
                .WithRequired(child => child.ProductComments2)
                .HasForeignKey(child => child.ParentId);
        }


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
        public DbSet<SelectedProductGroup> SelectedProductGroup { get; set; }
        public DbSet<SiteVisit> SiteVisits { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Cities> Cities { get; set; }
    }
}
