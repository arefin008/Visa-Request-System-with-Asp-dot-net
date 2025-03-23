using System;
using System.Web.Mvc;
using VisaRequestSystem.Models;

namespace VisaRequestSystem.Controllers
{
    public class TokenController : Controller
    {
        // GET: Token
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateToken(string visaType)
        {
            string token = TokenManager.GetToken(visaType);
            ViewBag.Token = token;
            ViewBag.Timestamp = DateTime.Now.ToString("g");
            return View("Index");
        }

        public ActionResult Report()
        {
            var report = TokenManager.GetReport();
            return View(report);
        }
    }
}
