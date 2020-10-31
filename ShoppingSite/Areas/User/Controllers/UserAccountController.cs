using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Utilities;

namespace ShoppingSite.Areas.User.Controllers
{
    [Authorize(Roles = "user")]
    [RouteArea("User", AreaPrefix = "User")]
    public class UserAccountController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        [Route("UserInfo")]
        public ActionResult UserInfo()
        {
            return View();
        }



        [Route("ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public ActionResult ChangePassword([Bind(Include = "OldPassword,Password,PasswordConfirmation")]ChangePasswordViewModel model)
        {
            var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);

            if (!AccountsUtilities.VerifyHashPassword(user.Password, model.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "رمز عبور وارد شده صحیح نیست.");
                return View();
            }

            if (AccountsUtilities.VerifyHashPassword(user.Password,model.Password))
            {
                ModelState.AddModelError("Password", "رمز عبور جدید نمی‌تواند با رمز عبور فعلی یکسان باشد.");
                return View();
            }
            user.Password = model.Password.HashPassword();
            db.Save();
            ViewBag.IsSuccess = true;
            return View();
        }
       
    }
}