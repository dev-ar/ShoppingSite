using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,Email,Password,PasswordConfirmation")] RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (db.CustomRepository.ExistCheck(register.Email))
                {
                    var user = new Users()
                    {
                        UserName = register.UserName.Trim(),
                        Email = register.Email.Trim().ToLower(),
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

                    SendEmail.Send(user.Email, "فعالسازی حساب کاربری فروشگاه بامیلو", body);

                    return View("SuccessRegister", user);
                }
                ModelState.AddModelError("Email", "ایمیل قبلا در سیستم ثبت شده است.");
            }
            return View(register);
        }



        [Route("Login")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Email,Password,RememberMe")] LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                if (db.CustomRepository.LoginCheck(login.Email, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Email, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
            }
            ModelState.AddModelError("Email", "ایمیل یا رمز عبور شما صحیح نمی‌باشد.");
            return View();
        }

        [Route("LogOff")]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
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