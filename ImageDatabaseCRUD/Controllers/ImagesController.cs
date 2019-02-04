using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageDatabaseCRUD.Models;

namespace ImageDatabaseCRUD.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        public ActionResult Index()
        {
            var result = db.Images.ToList();

            var imageViewModels = new List<ImageViewModel>();
            foreach (var item in result)
            {
                imageViewModels.Add(new ImageViewModel
                {
                    Id = item.Id,
                    FileName = item.FileName,
                    ImageData = item.ImageData
                });
            }
            return View(imageViewModels);
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            var imageViewModel = new ImageViewModel
            {
                Id = image.Id,
                FileName = image.FileName,
                ImageData = image.ImageData
            };

            return View(imageViewModel);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,ImageData")] HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    FileName = file.FileName
                };

                byte[] data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);

                image.ImageData = data;

                var imageViewModel = new ImageViewModel
                {
                    FileName = file.FileName,
                    ImageData = data,
                };


                db.Images.Add(image);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            //return View(image);
            return RedirectToAction("Create");
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            var imageViewModel = new ImageViewModel
            {
                Id = image.Id,
                FileName = image.FileName,
                ImageData = image.ImageData
            };
            return View(imageViewModel);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,ImageData")] int id, HttpPostedFileBase file)
        {
            if(file == null)
            {
                var image = db.Images.Find(id);
                var imageViewModel = new ImageViewModel
                {
                    Id = id,
                    FileName = image.FileName,
                    ImageData = image.ImageData
                };
                
                return View(imageViewModel);
            }
                

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    Id = id,
                    FileName = file.FileName
                };

                byte[] data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);
                image.ImageData = data;

                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", id);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            var imageViewModel = new ImageViewModel
            {
                Id = image.Id,
                FileName = image.FileName,
                ImageData = image.ImageData
            };

            return View(imageViewModel);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
