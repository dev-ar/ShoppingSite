using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain;
using Utilities;

namespace Data
{
    public class CustomRepository: ICustomRepository
    {
        private ShopSiteDB db;
        public CustomRepository(ShopSiteDB context) => db = context;


        public bool ExistCheck( string email)
        {
            if (db.Users.Any(u => u.Email ==  email.Trim().ToLower()))
                return false;
            return true;
        }


        public Users ActiveCodeCheck(string id)
        {
            return db.Users.SingleOrDefault(u => u.ActiveCode == id);
        }

        public bool LoginCheck(string email, string password)
        {
            var user = db.Users.SingleOrDefault( u => u.Email.ToLower() == email.Trim().ToLower());
            return user != null && AccountsUtilities.VerifyHashPassword(user.Password,password);
        }
    }
}
