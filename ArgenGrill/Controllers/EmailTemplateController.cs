using System.Web.Mvc;

namespace ArgenGrill.Controllers
{
    public class EmailTemplateController : Controller
    {
        // GET: EmailTemplate
        public ActionResult WelcomeEmail(Models.EmailViewModel myModel)
        {
            return View(myModel);
        }
    }
}