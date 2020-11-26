using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopSiteDB db;

        public UnitOfWork(ShopSiteDB context) => db = context;


        private GenericRepository<Users> _usersRepository;
        public GenericRepository<Users> UsersRepository => _usersRepository ??= new GenericRepository<Users>(db);


        private GenericRepository<Roles> _rolesRepository;
        public GenericRepository<Roles> RolesRepository => _rolesRepository ??= new GenericRepository<Roles>(db);

        private GenericRepository<Orders> _ordersRepository;
        public GenericRepository<Orders> OrdersRepository => _ordersRepository ??= new GenericRepository<Orders>(db);


        private GenericRepository<OrderDetails> _orderDetailsRepository;
        public GenericRepository<OrderDetails> OrderDetailsRepository =>
            _orderDetailsRepository ??= new GenericRepository<OrderDetails>(db);


        private GenericRepository<Products> _productsRepository;
        public GenericRepository<Products> ProductsRepository => _productsRepository ??= new GenericRepository<Products>(db);

        private GenericRepository<ProductFeatures> _productFeaturesRepository;
        public GenericRepository<ProductFeatures> ProductFeaturesRepository =>
            _productFeaturesRepository ??= new GenericRepository<ProductFeatures>(db);

        private GenericRepository<Features> _featuresRepository;
        public GenericRepository<Features> FeaturesRepository =>
            _featuresRepository ??= new GenericRepository<Features>(db);


        private GenericRepository<ProductGroups> _productGroupsRepository;
        public GenericRepository<ProductGroups> ProductGroupsRepository =>
            _productGroupsRepository ??= new GenericRepository<ProductGroups>(db);

        private GenericRepository<SelectedProductGroup> _selectedProductGroupsRepository;
        public GenericRepository<SelectedProductGroup> SelectedProductGroupRepository =>
            _selectedProductGroupsRepository ??= new GenericRepository<SelectedProductGroup>(db);

        private GenericRepository<ProductGalleries> _productGalleryRepository;
        public GenericRepository<ProductGalleries> ProductGalleriesRepository =>
            _productGalleryRepository ??= new GenericRepository<ProductGalleries>(db);

        private GenericRepository<ProductTags> _productTagsRepository;

        public GenericRepository<ProductTags> ProductTagsRepository =>
            _productTagsRepository ??= new GenericRepository<ProductTags>(db);

        private GenericRepository<ProductComments> _productCommentsRepository;

        public GenericRepository<ProductComments> ProductCommentsRepository =>
            _productCommentsRepository ??= new GenericRepository<ProductComments>(db);

        private GenericRepository<SiteVisit> _siteVisitRepository;

        public GenericRepository<SiteVisit> SiteVisitRepository =>
            _siteVisitRepository ??= new GenericRepository<SiteVisit>(db);

        private GenericRepository<Slider> _sliderRepository;
        public GenericRepository<Slider> SliderRepository => _sliderRepository ??= new GenericRepository<Slider>(db);

        private GenericRepository<Addresses> _addressesRepository;
        public GenericRepository<Addresses> AddressesRepository => _addressesRepository ??= new GenericRepository<Addresses>(db);


        private GenericRepository<States> _stateRepository;
        public GenericRepository<States> StateRepository => _stateRepository ??= new GenericRepository<States>(db);

        private GenericRepository<Cities> _citiesRepository;
        public GenericRepository<Cities> CitiesRepository => _citiesRepository ??= new GenericRepository<Cities>(db);

        private AccountRepository _accountRepository;
        public AccountRepository AccountRepository => _accountRepository ??= new AccountRepository(db);

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
