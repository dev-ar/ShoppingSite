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
                           UserName = register.UserName.Trim(),
                           Email = register.Email.Trim(),
                           Password = register.Password.HashPassword(),
                           ActiveCode = Guid.NewGuid().ToString(),
                           IsActive = false,
                           RegisterDate = DateTime.Now,
                           RoleId = 1,
                           LastLoginDate = DateTime.Now,
                           LastLoginIp = AccountsUtilities.GetUserIp(),
                       };
                        db.UsersRepository.Insert(user);
                        db.Save();

                       var body = PartialToStringClass.RenderPartialView("ManageEmails", "ActivationEmail", user);

                       SendEmail.Send(user.Email,"فعالسازی حساب کاربری فروشگاه بامیلو", body);

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
  


        [Route("Login")]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Email,Password,RememberMe")] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (db.CustomRepository.LoginCheck(login.UserName, login.Password))
                {
                    return View("test");
                }
            }

            ModelState.AddModelError("UserName","نام کاربری/ایمیل یا رمز عبور شما صحیح نمی‌باشد.");
            return View();
        }


        [Route("ActiveUser/{id}")]
        public ActionResult ActiveUser(string id)
        {
            var user = db.CustomRepository.ActiveCodeCheck(id);
            if (user == null)
                return HttpNotFound();
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.Save();
            ViewBag.UserName = user.UserName;
            return View();
        }

    }
}