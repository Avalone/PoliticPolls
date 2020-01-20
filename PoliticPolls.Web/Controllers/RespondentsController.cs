using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
using System.Linq;

namespace PoliticPolls.Web.Controllers
{
    public class RespondentsController : Controller
    {
        private ApplicationDbContext db;

        public RespondentsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Respondents
        public ActionResult Index()
        {
            return View(db.Respondents.ToList());
        }

        // GET: Respondents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var respondents = db.Respondents.Find(id);
            if (respondents == null)
            {
                return NotFound();
            }
            return View(respondents);
        }

        // GET: Respondents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Respondents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id","Name","Surname","Patro","BirthDate")] Respondents respondents)
        {
            if (ModelState.IsValid)
            {
                db.Respondents.Add(respondents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(respondents);
        }

        // GET: Respondents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var respondents = db.Respondents.Find(id);
            if (respondents == null)
            {
                return NotFound();
            }
            return View(respondents);
        }

        // POST: Respondents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id","Name","Surname","Patro","BirthDate")] Respondents respondents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respondents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(respondents);
        }

        // GET: Respondents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var respondents = db.Respondents.Find(id);
            if (respondents == null)
            {
                return NotFound();
            }
            return View(respondents);
        }

        // POST: Respondents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var respondents = db.Respondents.Find(id);
            db.Respondents.Remove(respondents);
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
