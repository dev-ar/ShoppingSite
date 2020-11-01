﻿using System;
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
    }
}