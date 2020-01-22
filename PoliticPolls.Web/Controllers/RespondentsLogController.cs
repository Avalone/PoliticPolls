using Microsoft.AspNetCore.Mvc;
using PoliticPolls.DataModel;
using System.Linq;

namespace PoliticPolls.Web.Controllers
{
    public class RespondentsLogController : Controller
    {
        private ApplicationDbContext db;
        public RespondentsLogController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.RespondentsLog.ToList());
        }
    }
}