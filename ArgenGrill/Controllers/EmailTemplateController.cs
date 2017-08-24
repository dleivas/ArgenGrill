using RazorEngine;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RazorEngine.Templating;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArgenGrill.Models;

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