using MVC_Template.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Template.Controllers
{
    public class SupplierController : Controller
    {
        NorthwindContext _model = new NorthwindContext();
        // GET: Supplier
        public ActionResult Index()
        {
            List<Supplier> supplier = _model.Suppliers.ToList();
            return View(supplier);
        }

        public ActionResult AddSupplier()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {

            if (supplier == null)
                return HttpNotFound();


            _model.Suppliers.Add(supplier);
            _model.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSupplier(int? id)
        {
            if(id == null)
                return RedirectToAction("Index");

            Supplier supplier = _model.Suppliers.Find(id);
            if (supplier == null)
                return HttpNotFound();

            _model.Suppliers.Remove(supplier);
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
            Supplier supplier = _model.Suppliers.FirstOrDefault(x => x.SupplierID == id);
            if (supplier == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            _model.Suppliers.Remove(supplier);
            _model.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateSupplier(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Supplier supplier = _model.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier); 
        }
        [HttpPost]
        public ActionResult UpdateSupplier(Supplier spl)
        {
            //Supplier supplier = _model.Suppliers.Find(spl.SupplierID);


            //supplier.CompanyName = spl.CompanyName;
            //supplier.Address = spl.Address;
            //supplier.City = spl.City;
            //supplier.ContactName = spl.ContactName;
            //supplier.ContactTitle = spl.ContactTitle;
            //supplier.Country = spl.Country;
            //supplier.Fax = spl.Fax;
            //supplier.Phone = spl.Phone;
            //supplier.PostalCode = spl.PostalCode;
            //supplier.Region = spl.Region;
            //supplier.HomePage = spl.HomePage;

            _model.Entry(spl).State = System.Data.Entity.EntityState.Modified;
            _model.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}