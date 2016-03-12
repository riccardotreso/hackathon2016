using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTTTube.FTP;
using System.Threading.Tasks;
using NTTTube.DB;

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
                var path = Path.Combine(Server.MapPath("/Video/"), fileName);
                FileUpload.SaveAs(path);

                // Inserimento DB
                var id = Repository.InsertVideo(new NTTTube.Model.Video()
                {
                    path = "http://ntt-mediaservice.cloudapp.net/video/",
                    description = "Inserimento video in NTTTube",
                    username = "tresor",
                    title = "Video di Prova",
                    category = "Private",
                    channel = "Aziendale",
                    date = DateTime.Now
                });

                // FTP Async
                var byteArr = System.IO.File.ReadAllBytes(path);
                NTTTube.FTP.Helper.Upload(byteArr, id + ".mp4");
            }

            return RedirectToAction("UploadDocument");
        }
    }
}
