using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            _categoryService.Add(category);
            return Redirect("/Admin/Category/CategoryList");
        }

        public ActionResult CategoryList()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }

        public ActionResult CategoryUpdate(Guid id)
        {
            Category category = _categoryService.GetByID(id);
            CategoryDTO model = new CategoryDTO();
            model.ID = category.ID;
            model.CategoryName = category.CategoryName;
            model.CategoryDescription = category.CategoryDescription;
            return View(model);
        }
        [HttpPost]
        public ActionResult CategoryUpdate(Category category)
        {
            _categoryService.Update(category);
            return Redirect("/Admin/Category/CategoryList");
        }

        public ActionResult Delete(Guid id)
        {
            _categoryService.GetByID(id);
            return Redirect("/Admin/Category/CategoryList");
        }
    }
}