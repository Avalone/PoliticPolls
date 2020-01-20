using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace PoliticPolls.Web.Controllers
{
    public class PoliticianSetsController : Controller
    {
        //private PoliticPollsEntities db = new PoliticPollsEntities();
        private ApplicationDbContext db;

        public PoliticianSetsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: PoliticianSets
        public ActionResult Index()
        {
            var politician_sets = db.PoliticianSets.Include(p => p.Politician).Include(p => p.Poll); 
            return View(politician_sets.ToList());
        }

        // GET: PoliticianSets/Details/5
        public ActionResult Details(int? id_politician, int? id_poll)
        {
            if (!id_politician.HasValue || !id_poll.HasValue)
            {
                return BadRequest();
            }
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == id_politician.Value && x.IdPoll == id_poll).FirstOrDefault();
            if (politician_sets == null)
            {
                return NotFound();
            }
            return View(politician_sets);
        }

        // GET: PoliticianSets/Create
        public ActionResult Create()
        {
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname");
            ViewBag.id_poll = new SelectList(db.Poll, "Id", "Id");
            return View();
        }

        // POST: PoliticianSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdPoll","IdPolitician","Rating")] PoliticianSets politician_sets)
        {
            if (ModelState.IsValid)
            {
                db.PoliticianSets.Add(politician_sets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.id_poll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // GET: PoliticianSets/Edit/5
        public ActionResult Edit(int? id_politician, int? id_poll)
        {
            if (!id_politician.HasValue || !id_poll.HasValue)
            {
                return BadRequest();
            }
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == id_politician.Value && x.IdPoll == id_poll).FirstOrDefault();
            if (politician_sets == null)
            {
                return NotFound();
            }
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.id_poll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // POST: PoliticianSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdPoll","IdPolitician","Rating")] PoliticianSets politician_sets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politician_sets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.id_poll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // GET: PoliticianSets/Delete/5
        public ActionResult Delete(int? id_politician, int? id_poll)
        {
            if (!id_politician.HasValue || !id_poll.HasValue)
            {
                return BadRequest();
            }
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == id_politician.Value && x.IdPoll == id_poll).FirstOrDefault();
            if (politician_sets == null)
            {
                return NotFound();
            }
            return View(politician_sets);
        }

        // POST: PoliticianSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id_politician, int? id_poll)
        {
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == id_politician.Value && x.IdPoll == id_poll).FirstOrDefault();
            db.PoliticianSets.Remove(politician_sets);
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
