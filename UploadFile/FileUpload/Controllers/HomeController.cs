 using System;
 using System.IO;
 using System.Web;
using System.Web.Mvc;
using System.Net;


namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var uploadUrl = "ftp://ftp.box.com";
                var uploadFileName = file.FileName;
                var username = "username";
                var password = "password";
                Stream streamObj = file.InputStream;
                byte[] buffer = new byte[file.ContentLength];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                string ftpUrl = String.Format("{0}/{1}", uploadUrl, uploadFileName);
                var requestObj = WebRequest.Create(ftpUrl) as FtpWebRequest;
                if (requestObj != null)
                {
                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                    requestObj.Credentials = new NetworkCredential(username, password);
                    Stream requestStream = requestObj.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
            }
            else
            {
                ViewData["message"] = "Bir dosya seçiniz.";
            }
            return View();
        }
    }
}