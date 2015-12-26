using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using K1.Models;
using K1.DAO;


namespace K1.Controllers
{
    public class CatalogsController : Controller
    {
        private K1DBEntities db = new K1DBEntities();
        private DAOa d = new DAOa();
   

        // GET: Catalogs
        public ActionResult Index(String na)
        {
            var search = from s in db.Catalogs select s;

            if (!String.IsNullOrEmpty(na))
            {
                search = search.Where(c => c.idact.ToString().Contains(na));
            }

            return View(search);
            
        }

        // GET: Catalogs/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // GET: Catalogs/Create
        [Authorize(Roles = "Prorab")]
        public ActionResult Create()
        {
            ViewBag.idact = new SelectList(db.Acts, "Id", "Id");
            ViewBag.idstuff = new SelectList(db.Stuffs, "Id", "Id");
            return View();
        }

   




        // POST: Catalogs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prorab")]
        public ActionResult Create([Bind(Include = "Id,idstuff,idact,name,tech,date")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Catalogs.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idact = new SelectList(db.Acts, "Id", "name", catalog.idact);
            ViewBag.idstuff = new SelectList(db.Stuffs, "Id", "name", catalog.idstuff);
            return View(catalog);
        }

        // GET: Catalogs/Edit/5
        [Authorize(Roles = "Prorab")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.idact = new SelectList(db.Acts, "Id", "name", catalog.idact);
            ViewBag.idstuff = new SelectList(db.Stuffs, "Id", "name", catalog.idstuff);
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prorab")]
        public ActionResult Edit([Bind(Include = "Id,idstuff,idact,name,tech,date")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idact = new SelectList(db.Acts, "Id", "name", catalog.idact);
            ViewBag.idstuff = new SelectList(db.Stuffs, "Id", "name", catalog.idstuff);
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        [Authorize(Roles = "Prorab")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [Authorize(Roles = "Prorab")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalog catalog = db.Catalogs.Find(id);
            db.Catalogs.Remove(catalog);
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
