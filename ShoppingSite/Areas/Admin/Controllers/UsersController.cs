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

namespace ShoppingSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UnitOfWork db = new UnitOfWork(new ShopSiteDB());
        // GET: Admin/Users
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

        // GET: Admin/Users/Create
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
        public ActionResult Create([Bind(Include = "UserId,RoleId,UserName,Password,Email,ActiveCode,IsActive,RegisterDate,LastLoginIp,LastLoginDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.UsersRepository.Insert(users);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", users.RoleId);
            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", users.RoleId);
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,UserName,Password,Email,ActiveCode,IsActive,RegisterDate,LastLoginIp,LastLoginDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.UsersRepository.Update(users);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.RolesRepository.GetAll(), "RoleId", "RoleName", users.RoleId);
            return View(users);
        }

        // GET: Admin/Users/Delete/5
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
            return View(users);
        }

        
        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.UsersRepository.GetById(id);
            db.UsersRepository.Delete(users);
            db.Save();
            return RedirectToAction("Index");
        }

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
