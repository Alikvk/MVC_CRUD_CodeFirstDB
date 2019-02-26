using MVC_Template.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Template.Controllers
{
    public class ProductController : Controller
    {
        NorthwindContext _model = new NorthwindContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Product> product = _model.Products.ToList();
            return View(product);
        }
        public ActionResult AddProduct()
        {
            List<Category> ctg = _model.Categories.ToList();
            List<Supplier> spl = _model.Suppliers.ToList();

            ViewBag.Categories = ctg;
            ViewBag.Suppliers = spl;

            //
            return View();
            //return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {

            if (product == null)
            {
                return HttpNotFound();
            }
            _model.Products.Add(product);
            _model.SaveChanges();
            // return View();
            //Sayfayı yönlendirme için kullanılır
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = _model.Products.Find(id);

            if (product == null)
                return HttpNotFound();


            _model.Products.Remove(product);
            _model.SaveChanges();


            return RedirectToAction("Index");

        }

        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product prd = _model.Products.FirstOrDefault(x => x.ProductID == id);
            if (prd == null)
            {
                return HttpNotFound();
            }
            return View(prd);
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(Product p)
        {
            Product prd = _model.Products.FirstOrDefault(x => x.ProductID == p.ProductID);
            _model.Products.Remove(prd);
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
            Product product = _model.Products.FirstOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            _model.Products.Remove(product);
            _model.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        


        [HttpGet]
        public ActionResult UpdateProduct(int? id)
        {
            List<Category> ctg = _model.Categories.ToList();
            List<Supplier> spl = _model.Suppliers.ToList();

            ViewBag.Categories = ctg;
            ViewBag.Suppliers = spl;

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Product product = _model.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            
            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(int id)
        {
            Product product = _model.Products.Find(id);

            TryUpdateModel(product, "", new string[] { "ProductName", "UnitPrice", "UnitsInStock", "SupplierID","CategoryID" });

            //_model.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _model.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}