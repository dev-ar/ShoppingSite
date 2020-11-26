using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Context;
using Domain;
using Utilities;
using ViewModel;

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [RouteArea("Admin",AreaPrefix = "admin")]
    public class UsersController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        // GET: Admin/Users
        [Route("Users")]
        public ActionResult Index()
        {
            return View(db.UsersRepository.GetAll());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersRepository.GetById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [Route("Users/Add")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Add")]
        public ActionResult Create([Bind(Include = "RoleId,UserName,Password,PasswordConfirmation, Email,IsActive")] AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.AccountRepository.ExistCheck(model.Email) == false)
                {
                    ModelState.AddModelError("Email","این ایمیل در سیستم وجود دارد.");
                    ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", model.RoleId);
                    return View(model);
                }

                var user = new Users
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password.HashPassword(),
                    ActiveCode = Guid.NewGuid().ToString(),
                    IsActive = model.IsActive,
                    RoleId = model.RoleId,
                    RegisterDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    LastLoginIp = AccountsUtilities.GetUserIp(),
                };
                db.UsersRepository.Insert(user);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", model.RoleId);
            return View(model);
        }

        [Route("Users/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.UsersRepository.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var viewUser = new EditUserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                
            };


            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", user.RoleId);
            return View( viewUser);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Edit/{id}")]
        public ActionResult Edit([Bind(Include = "RoleId,UserName,Email,IsActive")] EditUserViewModel model, int id)
        {

            if (ModelState.IsValid)
            {
                var user = db.UsersRepository.GetById(id);

                if (db.AccountRepository.ExistCheck(model.Email) == false && user.Email != model.Email.Trim().ToLower())
                {
                    ModelState.AddModelError("Email", "این ایمیل در سیستم وجود دارد.");
                    ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", model.RoleId);
                    return View(model);
                }

                
                user.RoleId = model.RoleId;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.IsActive = model.IsActive;


                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", model.RoleId);
            return View(model);
        }

        // GET: Admin/Users/Delete/5
        [Route("Users/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.UsersRepository.GetById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }

        
        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Users/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.UsersRepository.GetById(id);

            foreach (var item in user.Addresses.ToList())
                db.AddressesRepository.Delete(item);



            db.UsersRepository.Delete(user);
            db.Save();
            return RedirectToAction("Index");
        }

        [Route("AdminUserName")]
        public ActionResult AdminUserName()
        {
            ViewBag.username = db.AccountRepository.GetUserNameByEmail(User.Identity.Name);
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
