using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTTTube.DB;

namespace NTTTube.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var video = Repository.GetAllVideo();

            return View("~/Views/Home.cshtml", video);
        }

        public ActionResult GridCompany()
        {
            return View("~/Views/Azienda.cshtml");
        }

        public ActionResult GridTraining()
        {
            return View("~/Views/Formazione.cshtml");
        }

        public ActionResult GridIdea()
        {
            return View("~/Views/Idea.cshtml");
        }

        public ActionResult GridPeople()
        {
            return View("~/Views/MiPresento.cshtml");
        }
    }
}
