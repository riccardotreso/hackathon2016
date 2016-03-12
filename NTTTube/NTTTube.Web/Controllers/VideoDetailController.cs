using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTTTube.DB;

namespace NTTTube.Web.Controllers
{
    public class VideoDetailController : Controller
    {
        //
        // GET: /VideoDetail/

        public ActionResult VideoDetailDocument(string id)
        {
            var video = Repository.GetVideo(id);

            return View("~/Views/VideoDetail.cshtml", video);
        }

    }
}
