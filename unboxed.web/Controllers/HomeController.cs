using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using unboxed.web.Models;

namespace unboxed.web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual async Task<ActionResult> Index()
        {
            var db = new UnboxedDbContext();

            var surveys = await db.Surveys
                .AsNoTracking()
                .ToListAsync();
            var model = surveys.Select(s => new PanelModel()
            {
                Title = s.Title,
                Body = "Some random awesome description",
                ButtonText = "Resume",
                ButtonTarget = MVC.Survey.Index(s.ExternalId)
            });

            return View(model);
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}