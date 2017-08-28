using Argengrill.Core;
using Argengrill.Infrastructure;
using ArgenGrill.Models;
using System.Web.Mvc;

namespace ArgenGrill.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private NewsletterRepository db = new NewsletterRepository();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewsletter(NewsletterViewModel NewsVModel)
        {
            if (ModelState.IsValid)
            {
                Newsletter News = new Newsletter();
                News.Email = NewsVModel.Email;
                db.Add(News);
                ViewBag.Message = "Thank you for sigining up!";
                return PartialView("_Newsletter");
            }
            ViewBag.Message = "Something happened, could not sign up!";
            return PartialView("_Newsletter");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}