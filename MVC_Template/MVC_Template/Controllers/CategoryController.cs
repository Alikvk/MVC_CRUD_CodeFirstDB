using MVC_Template.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Template.Controllers
{
    public class CategoryController : Controller
    {
        NorthwindContext _model = new NorthwindContext();
        // GET: Category
        public ActionResult Index()
        {
            List<Category> ctg = _model.Categories.ToList();

            // Model Yöntemi ile gönderiyoruz
            return View(ctg);
        }
        public ActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory([Bind(Include = "CategoryName, Description")] Category category, HttpPostedFileBase Picture)
        {

            if (Picture == null)
                return View();

            category.Picture = ConvertToBytes(Picture);

            if (ModelState.IsValid)
            {
                _model.Categories.Add(category);
                _model.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // Category Picture nesnesi Database'de byte[] şeklinde tutulduğu için seçilen resmi byte[]'e çevrilmesini sağlayan method.
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes(image.ContentLength);
            // OLE header = 78  Microsoft Access resimler için belirlediği header sayısı
            byte[] bytes = new byte[imageBytes.Length + 78];
            Array.Copy(imageBytes, 0, bytes, 78, imageBytes.Length);
            return bytes;
        }

        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Category category = _model.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            _model.Categories.Remove(category);

            _model.SaveChanges();

            return RedirectToAction("Index");
        }

        //Ajax işlemi ile silme işlemini  yaparak kullandığımız method
        [HttpPost]
        public JsonResult DeleteAjax(int? id)
        {
            if (id == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            Category category = _model.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (category == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            _model.Categories.Remove(category);
            _model.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Category category = _model.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            
            return View(category);
        }


        [HttpPost]
        // Bind içinde include olursa o değerleri almanı söylersin ama exclude olursa o değerlerden başka bütün değerleri getirir
        public ActionResult UpdateCategory([Bind(Exclude = "Picture")] Category category, HttpPostedFileBase Picture)
        {
            if (Picture == null)
            {
                return RedirectToAction("UpdateCategory");
            }
            category.Picture = ConvertToBytes(Picture);
            //Category ctg = _model.Categories.Find(category.CategoryID);

            //ctg.CategoryName = category.CategoryName;
            //ctg.Description = category.Description;
            //ctg.Picture = category.Picture;

            _model.Entry(category).State = EntityState.Modified;

            _model.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}