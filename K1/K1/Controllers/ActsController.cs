using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using K1.Models;

namespace K1.Controllers
{
    public class ActsController : Controller
    {
        private K1DBEntities db = new K1DBEntities();

        // GET: Acts
        public ActionResult Index()
        {
            var acts = db.Acts.Include(a => a.Act2);
            return View(acts.ToList());
        }

        // GET: Acts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // GET: Acts/Create
        [Authorize(Roles = "Accounter")]
        public ActionResult Create()
        {
            ViewBag.idref = new SelectList(db.Acts, "Id", "Id");
            return View();
        }

        // POST: Acts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult Create([Bind(Include = "Id,name,building,agreement,date,idref")] Act act)
        {
            if (ModelState.IsValid)
            {
                db.Acts.Add(act);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idref = new SelectList(db.Acts, "Id", "Id", act.idref);
            return View(act);
        }

        // GET: Acts/Edit/5
        [Authorize(Roles = "Accounter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            ViewBag.idref = new SelectList(db.Acts, "Id", "Id", act.idref);
            return View(act);
        }

        // POST: Acts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult Edit([Bind(Include = "Id,name,building,agreement,date,idref")] Act act)
        {
            if (ModelState.IsValid)
            {
                db.Entry(act).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idref = new SelectList(db.Acts, "Id", "Id", act.idref);
            return View(act);
        }

        // GET: Acts/Delete/5
        [Authorize(Roles = "Accounter")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act act = db.Acts.Find(id);
            if (act == null)
            {
                return HttpNotFound();
            }
            return View(act);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult DeleteConfirmed(int id)
        {
            Act act = db.Acts.Find(id);
            db.Acts.Remove(act);
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
