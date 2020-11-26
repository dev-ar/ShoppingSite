using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Context;
using Domain;
using Utilities;

namespace Data
{
    public class AccountRepository : IAccountRepository
    {
        private ShopSiteDB db;
        public AccountRepository(ShopSiteDB context) => db = context;


        public bool ExistCheck(string email)
        {
            if (db.Users.Any(u => u.Email == email.Trim().ToLower()))
                return false;
            return true;
        }


        public Users ActiveCodeCheck(string id)
        {
            return db.Users.SingleOrDefault(u => u.ActiveCode == id);
        }

        public bool LoginCheck(string email, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.Email.ToLower() == email.Trim().ToLower());
            return user != null && AccountsUtilities.VerifyHashPassword(user.Password, password);
        }

        public string GetUserNameByEmail(string email)
        {
            return db.Users.Single(u => u.Email == email.Trim().ToLower()).UserName;
        }

        public Users GetUserByEmail(string email)
        {
             return db.Users.SingleOrDefault(u => u.Email == email.Trim().ToLower());
        }

        public IEnumerable<Addresses> GetAddressByEmail(string email)
        {
            var user = db.Users.Single(u => u.Email == email.Trim().ToLower());
            return db.Addresses.Where(a => a.UserId == user.UserId);
        }

        public string GetStateById(int id)
        {
            return db.States.Single(s => s.StateId == id).StateTitle;
        }

        public IEnumerable<Cities> GetCitiesByStateId(int id)
        {
            return db.Cities.Where(s => s.StateId == id);
        }

        public IEnumerable<ProductGroups> GetMainProductGroups()
        {
            return db.ProductGroups.Where(g => g.ParentId == null);
        }

        public IEnumerable<ProductGroups> GetAllSubGroups(int groupId)
        {
            return db.ProductGroups.Where(g => g.ParentId == groupId);
        }

        public IEnumerable<ProductTags> GetTagsByProductId(int productId)
        {
            return db.ProductTags.Where(t => t.ProductId == productId);
        }

        public IEnumerable<SelectedProductGroup> GetSelectedPGsByProductId(int productId)
        {
            return db.SelectedProductGroup.Where(g => g.ProductId == productId);
        }
    }
}
