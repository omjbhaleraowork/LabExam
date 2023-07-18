using LabExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabExam.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> lstprod = Product.GetAllProducts();
            return View(lstprod);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product e = Product.GetSingleProduct(ViewBag.Id = id);
            return View(e);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod)
        {
            try
            {
                int count = Product.InserProduct(prod);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product emp = Product.GetSingleProduct(ViewBag.Id = id);
            return View(emp);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product prod)
        {
            try
            {
                int count = Product.Update(prod);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.message("Failed To Update");

                }

            }
            catch (Exception e)
            {

            }

            return View();
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product emp = Product.GetSingleProduct(ViewBag.Id = id);
            return View(emp);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int count = Product.Delete(ViewBag.Id = id);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.alert("Failed to delete");
                    return RedirectToAction(nameof(Delete));

                }
            }
            catch
            {
                return View();
            }
        }
    }
}