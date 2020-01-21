using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
using System.Data;
using System.Linq;

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
            var politician_sets = db.PoliticianSets.Include(p => p.Politician).Include(p => p.Poll).ThenInclude(p => p.Respondent);
            return View(politician_sets.ToList());
        }

        // GET: PoliticianSets/Details/5
        public ActionResult Details(decimal? IdPolitician, decimal? IdPoll)
        {
            if (!IdPolitician.HasValue || !IdPoll.HasValue)
            {
                return BadRequest();
            }
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == IdPolitician.Value && x.IdPoll == IdPoll).FirstOrDefault();
            if (politician_sets == null)
            {
                return NotFound();
            }
            return View(politician_sets);
        }

        // GET: PoliticianSets/Create
        public ActionResult Create()
        {
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname");
            ViewBag.IdPoll = new SelectList(db.Poll, "Id", "Id");
            return View();
        }

        // POST: PoliticianSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdPoll", "IdPolitician", "Rating")] PoliticianSets politician_sets)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_POLITICIAN_SETS(:id_poll, :id_politician, :rating, :result)",
                    new OracleParameter("id_poll", politician_sets.IdPoll),
                    new OracleParameter("id_politician", politician_sets.IdPolitician),
                    new OracleParameter("rating", politician_sets.Rating),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.IdPoll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // GET: PoliticianSets/Edit/5
        public ActionResult Edit(decimal? IdPolitician, decimal? IdPoll)
        {
            if (!IdPolitician.HasValue || !IdPoll.HasValue)
            {
                return BadRequest();
            }
            var politician_sets = db.PoliticianSets.Where(x => x.IdPolitician == IdPolitician.Value && x.IdPoll == IdPoll).FirstOrDefault();
            if (politician_sets == null)
            {
                return NotFound();
            }
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.IdPoll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // POST: PoliticianSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdPoll", "IdPolitician", "Rating")] PoliticianSets politician_sets)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "UPDATE_POLITICIAN_SETS(:id_poll, :id_politician, :rating, :result)",
                    new OracleParameter("id_poll", politician_sets.IdPoll),
                    new OracleParameter("id_politician", politician_sets.IdPolitician),
                    new OracleParameter("rating", politician_sets.Rating),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", politician_sets.IdPolitician);
            ViewBag.IdPoll = new SelectList(db.Poll, "Id", "Id", politician_sets.IdPoll);
            return View(politician_sets);
        }

        // GET: PoliticianSets/Delete/5
        public ActionResult Delete(decimal? id_politician, decimal? id_poll)
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
        public ActionResult DeleteConfirmed(decimal? id_politician, decimal? id_poll)
        {
            var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
            SqlUtility.ExecuteStoredProcedure(db, "DELETE_POLITICIAN_SETS(:id_poll, :id_politician, :result)",
                new OracleParameter("id_poll", id_poll),
                new OracleParameter("id_politician", id_politician),
                resParam);
            var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
            if (result < 0)
            {
                return BadRequest();
            }
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
