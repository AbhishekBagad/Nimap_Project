using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nimap_Project.Models;
using Nimap_Project.Services;

namespace Nimap_Project.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductServices productServices;
       
        private readonly ICategoryService categoryService ;

        public ProductController(IProductServices _productServices, ICategoryService _categoryService)
        {
            productServices = _productServices;
            categoryService = _categoryService;
          
        }
        public IActionResult Index(int pg=1)
        {
            var products= productServices.GetProducts();
            const int pagesize = 10;
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
            
        }

        public IActionResult Create()
        {
            ViewBag.Category = categoryService.GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            int result = 0;
            result= productServices.AddProduct(p);
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
            var e = productServices.GetProductById(id);
            ViewBag.Category = categoryService.GetCategories();
            return View(e);

        }
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            int result = 0;
            result= productServices.UpdateProduct(p);
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
            var d= productServices.GetProductById(id);
            return View(d);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            int result = 0;
            result = productServices.DeleteProduct(id);
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
            var det= productServices.GetProductById(id);
            return View(det);
        }
    }
}
