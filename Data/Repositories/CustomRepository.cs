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


        public char ExistCheck(string username, string email)
        {
            if (db.Users.Any(u => string.Equals(u.Email, email.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                return 'E';
            if (db.Users.Any(u => u.UserName.ToLower() == username.Trim().ToLower()))
                return 'U';

            return 'T';
        }

        public Users ActiveCodeCheck(string id)
        {
            return db.Users.SingleOrDefault(u => u.ActiveCode == id);
        }

        public bool LoginCheck(string username, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.UserName.ToLower() == username.Trim().ToLower() || u.Email.ToLower() == username.Trim().ToLower());
            return user != null && AccountsUtilities.VerifyHashPassword(user.Password,password);
        }
    }
}
