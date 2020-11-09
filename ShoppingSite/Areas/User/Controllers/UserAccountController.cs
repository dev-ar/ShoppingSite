using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;
using Utilities;
using ViewModel;

namespace ShoppingSite.Areas.User.Controllers
{
    [Authorize]
    [RouteArea("User", AreaPrefix = "User")]
    public class UserAccountController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());

        [Route("UserInfo")]
        public ActionResult UserInfo()
        {
            return View(db.AccountRepository.GetUserByEmail(User.Identity.Name));
        }

        [Route("GetUserName")]
        [ChildActionOnly]
        public ActionResult GetUserName()
        {
            ViewBag.Username = db.AccountRepository.GetUserNameByEmail(User.Identity.Name);
            return PartialView();
        }

        [Route("Edit")]
        public ActionResult Edit()
        {
            var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public ActionResult Edit(Users model)
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

        [Route("Address")]
        public ActionResult ShowAddresses()
        {
            return View(db.AccountRepository.GetAddressByEmail(User.Identity.Name));
        }


        [Route("AddAddress")]
        public ActionResult AddAddress()
        {
            ViewBag.State = new SelectList(db.StateRepository.GetAll(), "StateId", "StateTitle");
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AddAddress")]
        public ActionResult AddAddress([Bind(Include = "State,Cities,ReceiverName,PhoneNumber,Address,PlateNumber,ZipCode,IdentificationCode")] AddAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.AccountRepository.GetUserByEmail(User.Identity.Name);
                var state = db.AccountRepository.GetStateById(Convert.ToInt32(model.State));
                var address = new Addresses
                {
                    UserId = user.UserId,
                    City = state +"، " + model.Cities,
                    ReceiverName = model.ReceiverName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PlateNumber = model.PlateNumber,
                    ZipCode = model.ZipCode,
                    IdentificationCode = model.IdentificationCode,
                };
                db.AddressesRepository.Insert(address);
                db.Save();
                return Redirect("/user/address?added=true");
            }

            ViewBag.State = new SelectList(db.StateRepository.GetAll(), "StateId", "StateTitle");
            return View(model);
        }


        [Route("GetCities/{id}")]
        public ActionResult GetCities(int id)
        {
            var data = db.AccountRepository.GetCitiesByStateId(id)
                .Select(c => new {Text = c.CityTitle, Value = c.CityTitle});
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}