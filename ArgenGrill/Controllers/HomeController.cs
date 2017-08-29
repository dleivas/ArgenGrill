using Argengrill.Core;
using Argengrill.Infrastructure;
using ArgenGrill.Models;
using System.Threading.Tasks;
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
        // POST: /Account/CreateNewsletter
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewsletter(NewsletterViewModel News)
        {
            Newsletter MyNews = new Newsletter();
            MyNews.Email = News.Email;

            if (ModelState.IsValid)
            {
                if (await db.EmailExists(MyNews.Email))
                {
                    ViewBag.Message = "This email has already been registered for newsletters!";
                    return PartialView("_Newsletter");
                }

                await db.Add(MyNews);
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