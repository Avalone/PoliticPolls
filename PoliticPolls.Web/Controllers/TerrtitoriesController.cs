using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
using System;
using System.Linq;

namespace PoliticPolls.Web.Controllers
{
    public class TerrtitoriesController : Controller
    {
        private ApplicationDbContext db;

        public TerrtitoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Terrtitories
        public ActionResult Index()
        {
            return View(db.Terrtitory.ToList());
        }

        // GET: Terrtitories/Details/5
        public ActionResult Details(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var terrtitory = db.Terrtitory.Find(id);
            if (terrtitory == null)
            {
                return NotFound();
            }
            return View(terrtitory);
        }

        // GET: Terrtitories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Terrtitories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id","TerritoryName")] Terrtitory terrtitory)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_TERRITORY(:id, :name, :result)", new OracleParameter("id", terrtitory.Id), new OracleParameter("name", terrtitory.TerritoryName), resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                return RedirectToAction("Index");
            }

            return View(terrtitory);
        }

        // GET: Terrtitories/Edit/5
        public ActionResult Edit(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var terrtitory = db.Terrtitory.Find(id);
            if (terrtitory == null)
            {
                return NotFound();
            }
            return View(terrtitory);
        }

        // POST: Terrtitories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id","TerritoryName")] Terrtitory terrtitory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terrtitory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(terrtitory);
        }

        // GET: Terrtitories/Delete/5
        public ActionResult Delete(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var terrtitory = db.Terrtitory.Find(id);
            if (terrtitory == null)
            {
                return NotFound();
            }
            return View(terrtitory);
        }

        // POST: Terrtitories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            var terrtitory = db.Terrtitory.Find(id);
            db.Terrtitory.Remove(terrtitory);
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
