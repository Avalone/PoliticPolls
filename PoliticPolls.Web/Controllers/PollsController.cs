using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
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
        public ActionResult Details(decimal? id)
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
            ViewBag.IdRespondent = new SelectList(db.Respondents, "Id", "Surname");
            return View();
        }

        // POST: Polls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id", "PollDate", "IdRespondent")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_POLL(:id, :id_respondent, :poll_date, :result)",
                    new OracleParameter("id", poll.Id),
                    new OracleParameter("id_respondent", poll.IdRespondent),
                    new OracleParameter("poll_date", OracleDbType.Date, poll.PollDate, System.Data.ParameterDirection.Input),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdRespondent = new SelectList(db.Respondents, "id", "surname", poll.IdRespondent);
            return View(poll);
        }

        // GET: Polls/Edit/5
        public ActionResult Edit(decimal? id)
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
            ViewBag.IdRespondent = new SelectList(db.Respondents, "Id", "Surname", poll.IdRespondent);
            return View(poll);
        }

        // POST: Polls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id", "PollDate", "IdRespondent")] Poll poll)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "UPDATE_POLL(:id, :id_respondent, :poll_date, :result)",
                    new OracleParameter("id", poll.Id),
                    new OracleParameter("id_respondent", poll.IdRespondent),
                    new OracleParameter("poll_date", OracleDbType.Date, poll.PollDate, System.Data.ParameterDirection.Input),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdRespondent = new SelectList(db.Respondents, "Id", "Surname", poll.IdRespondent);
            return View(poll);
        }

        // GET: Polls/Delete/5
        public ActionResult Delete(decimal? id)
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
        public ActionResult DeleteConfirmed(decimal id)
        {
            var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
            SqlUtility.ExecuteStoredProcedure(db, "DELETE_POLL(:id, :result)", new OracleParameter("id", id), resParam);
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
