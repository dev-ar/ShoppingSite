using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;

namespace Data
{
    public class CustomRepository: ICustomRepository
    {
        private ShopSiteDB db;
        public CustomRepository(ShopSiteDB context) => db = context;


        public char ExistCheck(string username, string email)
        {
            if (db.Users.Any(u => u.Email == email.Trim().ToLower()))
                return 'E';
            if (db.Users.Any(u => u.UserName == username.Trim().ToLower()))
                return 'U';

            return 'T';
        }
    }
}
