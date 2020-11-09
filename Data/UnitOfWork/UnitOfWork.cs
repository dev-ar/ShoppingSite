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
        public GenericRepository<OrderDetails> OrderDetailsRepository => _orderDetailsRepository ??= new GenericRepository<OrderDetails>(db);


        private GenericRepository<Products> _productsRepository;
        public GenericRepository<Products> ProductsRepository => _productsRepository ??= new GenericRepository<Products>(db);


        public GenericRepository<ProductFeatures> ProductFeaturesRepository { get; }
        public GenericRepository<Features> FeaturesRepository { get; }
        public GenericRepository<ProductGroups> ProductGroups { get; }
        public GenericRepository<SelectedProductGroup> SelectedProductGroupRepository { get; }
        public GenericRepository<ProductGalleries> ProductGalleriesRepository { get; }
        public GenericRepository<ProductTags> ProductTagsRepository { get; }
        public GenericRepository<ProductComments> ProductCommentsRepository { get; }
        public GenericRepository<SiteVisit> SiteVisitRepository { get; }
        public GenericRepository<Slider> SliderRepository { get; }

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
