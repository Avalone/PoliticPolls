using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PoliticPolls.DataModel;
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
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname");
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
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
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
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
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
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_politician = new SelectList(db.Politicians, "Id", "Surname", orders.IdPolitician);
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
            var orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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