using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomWindowsAuthentication.Models;
using CustomWindowsAuthentication.Infraestructure.Attributes;

namespace CustomWindowsAuthentication.Controllers
{
    public class UsuarioPerfisController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: UsuarioPerfis
        public ActionResult Index()
        {
            var usuarioPerfis = db.UsuarioPerfis.Include(u => u.Perfil);
            return View(usuarioPerfis.ToList());
        }

        // GET: UsuarioPerfis/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfis.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfis/Create
        public ActionResult Create()
        {
            ViewBag.PerfilId = new SelectList(db.Perfis, "PerfilId", "Nome");
            return View();
        }

        // POST: UsuarioPerfis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioPerfilId,UsuarioLogin,PerfilId")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                usuarioPerfil.UsuarioPerfilId = Guid.NewGuid();
                db.UsuarioPerfis.Add(usuarioPerfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PerfilId = new SelectList(db.Perfis, "PerfilId", "Nome", usuarioPerfil.PerfilId);
            return View(usuarioPerfil);
        }

        [CustomAuthorize(Roles = ("Administradores"))]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfis.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerfilId = new SelectList(db.Perfis, "PerfilId", "Nome", usuarioPerfil.PerfilId);
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioPerfilId,UsuarioLogin,PerfilId")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioPerfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PerfilId = new SelectList(db.Perfis, "PerfilId", "Nome", usuarioPerfil.PerfilId);
            return View(usuarioPerfil);
        }

        // GET: UsuarioPerfis/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfis.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // POST: UsuarioPerfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfis.Find(id);
            db.UsuarioPerfis.Remove(usuarioPerfil);
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
