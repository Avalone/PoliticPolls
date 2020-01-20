using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
using System.Linq;

namespace PoliticPolls.Web.Controllers
{
    public class PollsController : Controller
    {
        private ApplicationDbContext db;

        public PollsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Polls
        public ActionResult Index()
        {
            var poll = db.Poll.Include(p => p.Respondent);
            return View(poll.ToList());
        }

        // GET: Polls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var poll = db.Poll.Find(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        // GET: Polls/Create
        public ActionResult Create()
        {
            ViewBag.id_respondent = new SelectList(db.Respondents, "Id", "Surname");
            return View();
        }

        // POST: Polls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("id","PollDate","IdRespondent")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                db.Poll.Add(poll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_respondent = new SelectList(db.Respondents, "id", "surname", poll.IdRespondent);
            return View(poll);
        }

        // GET: Polls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var poll = db.Poll.Find(id);
            if (poll == null)
            {
                return NotFound();
            }
            ViewBag.id_respondent = new SelectList(db.Respondents, "Id", "Surname", poll.IdRespondent);
            return View(poll);
        }

        // POST: Polls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id","PollDate","IdRespondent")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_respondent = new SelectList(db.Respondents, "Id", "Surname", poll.IdRespondent);
            return View(poll);
        }

        // GET: Polls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var poll = db.Poll.Find(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        // POST: Polls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var poll = db.Poll.Find(id);
            db.Poll.Remove(poll);
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
