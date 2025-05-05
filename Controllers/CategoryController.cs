using Microsoft.AspNetCore.Mvc;
using Nimap_Project.Models;
using Nimap_Project.Services;

namespace Nimap_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;

        }    

        public IActionResult Index(int pg = 1)
        {
            var list = categoryService.GetCategories();
            const int pagesize = 10;
            if (pg < 1)
            {
                pg = 1;
            }
            int recscount = list.Count();
            var pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = list.Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
           
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            int result = 0;
            result= categoryService.AddCategory(cat);
            if (result >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Category not added";
                return View();
            }
          
            
        }

        public IActionResult Edit(int id)
        {
            var e= categoryService.GetCategoryById(id);
            return View(e);
        }
        [HttpPost]
        public IActionResult Edit(Category c)
        {
            int result = 0;
            result= categoryService.UpdateCategory(c);
            if (result >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = " Category not Updated";
                return View();
            }
           
        }

        public IActionResult Delete(int id)
        {
            var del= categoryService.GetCategoryById(id);
            return View(del);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int result= categoryService.DeleteCategory(id);
            if (result >= 1) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Category not Deleted";
                return View();
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var det= categoryService.GetCategoryById(id);
            return View(det);
        }

    }
}
