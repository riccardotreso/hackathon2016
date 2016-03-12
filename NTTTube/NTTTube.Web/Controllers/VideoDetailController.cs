using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTTTube.Web.Controllers
{
    public class VideoDetailController : Controller
    {
        //
        // GET: /VideoDetail/

        public ActionResult VideoDetailDocument()
        {
            return View("~/Views/VideoDetail.cshtml");
        }

    }
}
