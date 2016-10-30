using DomainModel.Models;
using DomainModel.Repositories;
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

        private FileRepository FileRepository { get; set;}
        public FileController()
        {
            FileRepository = new FileRepository();
        }
        
        public ActionResult Index()
        {
            return View(FileRepository.GetList());
        }
        
        public ActionResult Get(Guid idFile)
        {
            var file = FileRepository.Get(idFile);

            FileContentResult result = new FileContentResult(file.FileData, "application/octet-stream");
            result.FileDownloadName = file.Title;

            return result;
        }

        public ActionResult Delete(Guid idFile)
        {
            FileRepository.Delete(idFile);
            return RedirectToAction("Index", "File");
        }

        // GET: File
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FileCreateModel model)
        {
            Guid g;
            g = Guid.NewGuid();
            var file = Request.Files[0];
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

            return RedirectToAction("Index", "File");
        }
        
        public ActionResult Find(string attribute, string searchline)
        {
            List<SelectListItem> dropdownItems = new List<SelectListItem>();
            dropdownItems.AddRange(new[]{
                            new SelectListItem() { Text = "Title", Value = "Title" },
                            new SelectListItem() { Text = "CreationAuthor", Value = "CreationAuthor" },
                            new SelectListItem() { Text = "CreationDate", Value = "CreationDate" }});
            ViewData.Add("Attributes", dropdownItems);

            var findresults = string.IsNullOrWhiteSpace(attribute) ? FileRepository.GetList() : FileRepository.Find(attribute, searchline);

            return View(findresults);
        }
    }
}