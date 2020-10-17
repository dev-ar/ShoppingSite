using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain;
using Data.Context;
using Utilities;

namespace ShoppingSite.Controllers
{
    public class AccountsController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        // GET: Accounts

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,Email,Password,PasswordConfirmation")] RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                switch (db.CustomRepository.ExistCheck(register.UserName,register.Email))
                {
                    case 'T':
                       var user = new Users()
                       {
                           UserName = register.UserName.Trim().ToLower(),
                           Email = register.Email.Trim().ToLower(),
                           Password = register.Password.HashPassword(),
                           ActiveCode = Guid.NewGuid().ToString(),
                           IsActive = false,
                           RegisterDate = DateTime.Now,
                           RoleId = 1,
                           LastLoginDate = DateTime.Now,
                           LastLoginIp = GetUserIp(),
                       };
                        db.UsersRepository.Insert(user);
                        db.Save();
                       return View("SuccessRegister",user);
                    case 'E':
                        ModelState.AddModelError("Email", "ایمیل قبلا در سیستم ثبت شده است.");
                        break;
                    case 'U':
                        ModelState.AddModelError("UserName","نام کاربری در سیستم وجود دارد.");
                        break;
                }
              
            }
            return View(register);
        }
  



        public ActionResult Login()
        {
            
            var userIp = GetUserIp();
            return View();
        }




        private string GetUserIp()
        {
            var ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            return !string.IsNullOrEmpty(ipList) ? ipList.Split(',')[0] : Request.ServerVariables["REMOTE_ADDR"];
        }
        
    }
}