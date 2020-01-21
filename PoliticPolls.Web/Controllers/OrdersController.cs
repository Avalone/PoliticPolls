using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using PoliticPolls.DataModel;
using PoliticPolls.Web.Services;
using System.Linq;

namespace PoliticPolls.Web.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db;

        public OrdersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Politician).ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id", "Text", "IdPolitician")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "INSERT_ORDERS(:id, :text, :id_politician, :result)",
                    new OracleParameter("id", orders.Id),
                    new OracleParameter("text", orders.Text),
                    new OracleParameter("id_politician", orders.IdPolitician),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id", "Text", "IdPolitician")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
                SqlUtility.ExecuteStoredProcedure(db, "UPDATE_ORDERS(:id, :text, :id_politician, :result)",
                    new OracleParameter("id", orders.Id),
                    new OracleParameter("text", orders.Text),
                    new OracleParameter("id_politician", orders.IdPolitician),
                    resParam);
                var result = ((Oracle.ManagedDataAccess.Types.OracleDecimal)resParam.Value).Value;
                if (result < 0)
                {
                    return BadRequest();
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdPolitician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(decimal? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            var resParam = new OracleParameter("result", OracleDbType.Decimal, System.Data.ParameterDirection.Output);
            SqlUtility.ExecuteStoredProcedure(db, "DELETE_ORDERS(:id, :result)", new OracleParameter("id", id), resParam);
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