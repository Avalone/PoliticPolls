using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
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
        public ActionResult Details(decimal? id)
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
        public ActionResult Create([Bind("Id", "Name", "Surname", "Patro", "BirthDate")] Respondents respondents)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_RESPONDENTS(:id, :name, :surname, :patro, :birthdate, :result)",
                    new OracleParameter("id", respondents.Id),
                    new OracleParameter("name", respondents.Name),
                    new OracleParameter("surname", respondents.Surname),
                    new OracleParameter("patro", respondents.Patro),
                    new OracleParameter("birthdate", OracleDbType.Date, respondents.BirthDate, System.Data.ParameterDirection.Input),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }

            return View(respondents);
        }

        // GET: Respondents/Edit/5
        public ActionResult Edit(decimal? id)
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
        public ActionResult Edit([Bind("Id", "Name", "Surname", "Patro", "BirthDate")] Respondents respondents)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "UPDATE_RESPONDENTS(:id, :name, :surname, :patro, :birthdate, :result)",
                    new OracleParameter("id", respondents.Id),
                    new OracleParameter("name", respondents.Name),
                    new OracleParameter("surname", respondents.Surname),
                    new OracleParameter("patro", respondents.Patro),
                    new OracleParameter("birthdate", OracleDbType.Date, respondents.BirthDate, System.Data.ParameterDirection.Input),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }
            return View(respondents);
        }

        // GET: Respondents/Delete/5
        public ActionResult Delete(decimal? id)
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
        public ActionResult DeleteConfirmed(decimal id)
        {
            var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
            SqlUtility.ExecuteStoredProcedure(db, "DELETE_RESPONDENTS(:id, :result)", new OracleParameter("id", id), resParam);
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
