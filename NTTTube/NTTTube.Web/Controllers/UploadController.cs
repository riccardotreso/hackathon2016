using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTTTube.Web.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult UploadDocument()
        {
            return View("~/Views/Upload.cshtml");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase FileUpload)
        {
            System.Threading.Thread.Sleep(5000);

            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(FileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Video/"), fileName);
                FileUpload.SaveAs(path);
            }

            // FTP Async


            return RedirectToAction("UploadDocument");
        }

    }
}
