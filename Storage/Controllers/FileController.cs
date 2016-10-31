using DomainModel.Models;
using DomainModel.Repositories;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Storage.Controllers
{
    public class FileController : Controller
    {
        private bool Authorized()
        {
            return System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        private FileRepository FileRepository { get; set;}
        public FileController()
        {
            FileRepository = new FileRepository();
        }
        
        public ActionResult Index(string orderby, string direction)
        {
            if (Authorized())
            {
                return View(FileRepository.GetList(orderby, direction));
            }
            else return RedirectToAction("Login", "Account");
        }
        
        public ActionResult Get(Guid idFile)
        {
            if (Authorized())
            {
                var file = FileRepository.Get(idFile);

                FileContentResult result = new FileContentResult(file.FileData, "application/octet-stream");
                result.FileDownloadName = file.Title;

                return result;
            }
            else return RedirectToAction("Login", "Account");
        }

        public ActionResult Delete(Guid idFile)
        {
            if (Authorized())
            {
                FileRepository.Delete(idFile);
                return RedirectToAction("Index", "File");
            }
            else return RedirectToAction("Login", "Account");
        }

        // GET: File
        [HttpGet]
        public ActionResult Add()
        {
            if (Authorized())
            {
                return View();
            }
            else return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Add(FileCreateModel model)
        {
            if (Authorized())
            {
                Guid g;
                g = Guid.NewGuid();
                var file = Request.Files[0];
                if (Request.Files.Count > 0)
                {
                    byte[] filedata = null;
                    using (BinaryReader b = new BinaryReader(file.InputStream))
                    {
                        filedata = b.ReadBytes(file.ContentLength);
                    }
                    model.Title = file.FileName;
                    model.FileData = filedata;
                    model.CreationAuthor = User.Identity.Name;
                    model.CreationDate = DateTime.Now;
                    model.IdFile = g;
                    FileRepository.Add(model);
                }

                return RedirectToAction("Index", "File");
            }
            else return RedirectToAction("Login", "Account");
        }
        
        
        public ActionResult Find(string attribute, string searchline, DateTime? dateattribute)
        {
            if (Authorized())
            {
                var findresults = string.IsNullOrWhiteSpace(attribute) ? FileRepository.RowList() : FileRepository.Find(attribute, searchline, dateattribute);

                return View(findresults);
            }
            else return RedirectToAction("Login", "Account");
        }
    }
}