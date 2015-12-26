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

    
    public class StuffsController : Controller
    {
        private K1DBEntities db = new K1DBEntities();
        private DAOa v = new DAOa();
        // GET: Stuffs
        public ActionResult Index(String na)
        {
            var search = from s in db.Stuffs select s;
            if (!String.IsNullOrEmpty(na))
            {
                search = search.Where(c => c.name.Contains(na));
            }
            return View(search);
        }



        // GET: Stuffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuffs.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            return View(stuff);
        }

        // GET: Stuffs/Create
        [Authorize(Roles = "Accounter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stuffs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult Create([Bind(Include = "Id,name,properties,amount")] Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                db.Stuffs.Add(stuff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stuff);
        }

        // POST: Stuffs/Create1
    
        
        [Authorize(Roles = "Accounter")]
        // GET: Stuffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuffs.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            return View(stuff);
        }

        // POST: Stuffs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult Edit([Bind(Include = "Id,name,properties,amount")] Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stuff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stuff);
        }

        // GET: Stuffs/Delete/5
        [Authorize(Roles = "Accounter")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stuff stuff = db.Stuffs.Find(id);
            if (stuff == null)
            {
                return HttpNotFound();
            }
            return View(stuff);
        }

        // POST: Stuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accounter")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stuff stuff = db.Stuffs.Find(id);
            db.Stuffs.Remove(stuff);
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
