using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PoliticPolls.Web.Controllers
{
    public class PoliticiansController : Controller
    {
        //private PoliticPollsEntities db = new PoliticPollsEntities();
        private ApplicationDbContext db;

        public PoliticiansController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Politicians
        public ActionResult Index()
        {
            var politicians = db.Politicians;//.Include(p => p.Terrtitory);
            return View(politicians.ToList());
        }

        // GET: Politicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var politicians = db.Politicians.Find(id);
            if (politicians == null)
            {
                return NotFound();
            }
            return View(politicians);
        }

        // GET: Politicians/Create
        public ActionResult Create()
        {
            ViewBag.id_territory = new SelectList(db.Terrtitory, "Id", "TerritoryName");
            return View();
        }

        // POST: Politicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id","Name","Surname","Patro","IdTerritory")] Politicians politicians)
        {
            if (ModelState.IsValid)
            {
                db.Politicians.Add(politicians);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_territory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
            return View(politicians);
        }

        // GET: Politicians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var politicians = db.Politicians.Find(id);
            if (politicians == null)
            {
                return NotFound();
            }
            ViewBag.id_territory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
            return View(politicians);
        }

        // POST: Politicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id","Name","Surname","Patro","IdTerritory")] Politicians politicians)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politicians).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_territory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
            return View(politicians);
        }

        // GET: Politicians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var politicians = db.Politicians.Find(id);
            if (politicians == null)
            {
                return NotFound();
            }
            return View(politicians);
        }

        // POST: Politicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var politicians = db.Politicians.Find(id);
            db.Politicians.Remove(politicians);
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
