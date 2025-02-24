using Microsoft.AspNetCore.Mvc;
using Nimap_Project.Models;

namespace Nimap_Project.Controllers
{
    public class CategoryController : Controller
    {
        public readonly IConfiguration Configuration;
        private CategoryCrud db;
        public CategoryController(IConfiguration configuration)
        {
            this.Configuration = configuration;
            db=new CategoryCrud(this.Configuration);

        }    

        public IActionResult Index(int pg = 1)
        {
            var list = db.GetCategory();
            const int pagesize = 4;
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
            result=db.AddCategory(cat);
            if (result >= 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Category not added";
                return View();
            }
          
            return View();
        }

        public IActionResult Edit(int id)
        {
            var e=db.GetCategoryById(id);
            return View(e);
        }
        [HttpPost]
        public IActionResult Edit(Category c)
        {
            int result = 0;
            result=db.updateCategory(c);
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
            var del=db.GetCategoryById(id);
            return View(del);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int result=db.deleteCategory(id);
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
            var det=db.GetCategoryById(id);
            return View(det);
        }

    }
}
