using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccountingDemoApp.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categories;
        public CategoryController()
        {

        }
        public CategoryController(ICategoryService categories)
        {
            _categories = categories;
        }
        public async Task<ActionResult> Index()
        {
            var items = await _categories.GetCategories();
            var categories = new List<CategoryViewModel>();
            foreach (var i in items)
            {
                var category = new CategoryViewModel { Id = i.Id, Name = i.Name };
                categories.Add(category);
            }
            return View(categories);
        }

        public async Task<ActionResult> Details(int id)
        {
            var item = await _categories.GetCategory(id);
            var category = new CategoryViewModel { Id = item.Id, Name = item.Name };
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")]CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new CategoryDTO { Name = model.Name };
                var result = await _categories.Add(category);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError(result.Property, result.Message);
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _categories.GetCategory(id);
            var category = new CategoryViewModel { Id = item.Id, Name = item.Name };
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new CategoryDTO { Id = model.Id, Name = model.Name };
                var result = await _categories.Update(category);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError(result.Property, result.Message);
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id, string message)
        {
            var item = await _categories.GetCategory(id);
            var category = new CategoryViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.ErrorMessage = message;
            }
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(CategoryViewModel model)
        {
            var result = await _categories.Delete(model.Id);
            if (result.Succeeded)
                return RedirectToAction("Index");
            return RedirectToAction("Delete", new { message = result.Message });
        }
    }
}