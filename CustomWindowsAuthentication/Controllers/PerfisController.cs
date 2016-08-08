using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomWindowsAuthentication.Models;

namespace CustomWindowsAuthentication.Controllers
{
    public class PerfisController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Perfis
        public ActionResult Index()
        {
            return View(db.Perfis.ToList());
        }

        // GET: Perfis/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil perfil = db.Perfis.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // GET: Perfis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerfilId,Nome")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                perfil.PerfilId = Guid.NewGuid();
                db.Perfis.Add(perfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(perfil);
        }

        // GET: Perfis/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil perfil = db.Perfis.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerfilId,Nome")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(perfil);
        }

        // GET: Perfis/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil perfil = db.Perfis.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Perfil perfil = db.Perfis.Find(id);
            db.Perfis.Remove(perfil);
            db.SaveChanges();
            return RedirectToAction("Index");
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
