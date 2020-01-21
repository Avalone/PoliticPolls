using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
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
            var politicians = db.Politicians.Include(p => p.Terrtitory);
            return View(politicians.ToList());
        }

        // GET: Politicians/Details/5
        public ActionResult Details(decimal? id)
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
            ViewBag.IdTerritory = new SelectList(db.Terrtitory, "Id", "TerritoryName");
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
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_POLITICIANS(:id, :name, :surname, :patro, :id_territory, :result)",
                    new OracleParameter("id", politicians.Id),
                    new OracleParameter("name", politicians.Name),
                    new OracleParameter("surname", politicians.Surname),
                    new OracleParameter("patro", politicians.Patro),
                    new OracleParameter("id_territory", politicians.IdTerritory),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdTerritory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
            return View(politicians);
        }

        // GET: Politicians/Edit/5
        public ActionResult Edit(decimal? id)
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
            ViewBag.IdTerritory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
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
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "UPDATE_POLITICIANS(:id, :name, :surname, :patro, :id_territory, :result)",
                    new OracleParameter("id", politicians.Id),
                    new OracleParameter("name", politicians.Name),
                    new OracleParameter("surname", politicians.Surname),
                    new OracleParameter("patro", politicians.Patro),
                    new OracleParameter("id_territory", politicians.IdTerritory),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdTerritory = new SelectList(db.Terrtitory, "Id", "TerritoryName", politicians.IdTerritory);
            return View(politicians);
        }

        // GET: Politicians/Delete/5
        public ActionResult Delete(decimal? id)
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
        public ActionResult DeleteConfirmed(decimal id)
        {
            var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
            SqlUtility.ExecuteStoredProcedure(db, "DELETE_POLITICIANS(:id, :result)", new OracleParameter("id", id), resParam);
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
