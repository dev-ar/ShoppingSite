using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain;

namespace Data
{
    public interface IUnitOfWork
    {
        GenericRepository<Users> UsersRepository { get; }
        GenericRepository<Roles> RolesRepository { get; }
        GenericRepository<Orders> OrdersRepository { get; }
        GenericRepository<OrderDetails> OrderDetailsRepository { get; }
        GenericRepository<Products> ProductsRepository { get; }
        GenericRepository<ProductFeatures> ProductFeaturesRepository { get; }
        GenericRepository<Features> FeaturesRepository { get; }
        GenericRepository<ProductGroups> ProductGroups { get; }
        GenericRepository<SelectedProductGroup> SelectedProductGroupRepository  { get; }
        GenericRepository<ProductGalleries> ProductGalleriesRepository { get; }
        GenericRepository<ProductTags> ProductTagsRepository { get; }
        GenericRepository<ProductComments> ProductCommentsRepository { get; }
        GenericRepository<SiteVisit> SiteVisitRepository { get; }
        GenericRepository<Slider> SliderRepository { get; }
        AccountRepository AccountRepository { get; }
        void Save();
        void Dispose();
    }
}
