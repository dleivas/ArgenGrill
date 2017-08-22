using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArgenGrill.Controllers
{
    public class EmailTemplateController : Controller
    {
        // GET: EmailTemplate
        public ActionResult WelcomeEmail()
        {
            return View();
        }
    }
}