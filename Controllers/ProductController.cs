using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nimap_Project.Models;

namespace Nimap_Project.Controllers
{
    public class ProductController : Controller
    {
        public readonly IConfiguration Configuration;
        public ProductCrud cd;
        public CategoryCrud cat;

        public ProductController(IConfiguration configuration)
        {
            this.Configuration = configuration;
            cd=new ProductCrud(this.Configuration);
            cat = new CategoryCrud(this.Configuration);
        }
        public IActionResult Index(int pg=1)
        {
            var products=cd.GetProducts();
            const int pagesize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int recscount = products.Count();
            var pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = products.Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
            //var pro=cd.GetProducts();
            //return View(pro);
        }

        public IActionResult Create()
        {
            ViewBag.Category = cat.GetCategory();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            int result = 0;
            result=cd.AddProduct(p);
            if (result >= 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Product Not Added";
                return View();
            }
            
        }

        public IActionResult Edit(int id)
        {
            var e = cd.GetProductById(id);
            ViewBag.Category = cat.GetCategory();
            return View(e);

        }
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            int result = 0;
            result=cd.UpdateProduct(p);
            if (result >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Product Not Update";
                return View();
            }

        }

        public IActionResult Delete(int id)
        {
            var d=cd.GetProductById(id);
            return View(d);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            int result = 0;
            result = cd.DeleteProduct(id);
            if (result >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "product not deleted";
                return View();  
            }
        }

        public IActionResult Details(int id)
        {
            var det=cd.GetProductById(id);
            return View(det);
        }
    }
}
