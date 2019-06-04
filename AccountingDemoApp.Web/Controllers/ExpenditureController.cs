using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccountingDemoApp.Web.Controllers
{
    [Authorize]
    public class ExpenditureController : Controller
    {
        private readonly IExpenditureService _expenditures;
        private readonly ICategoryService _categories;
        public ExpenditureController()
        {

        }
        public ExpenditureController(IExpenditureService expenditures, ICategoryService categories)
        {
            _expenditures = expenditures;
            _categories = categories;
        }
        // GET: Expenditure
        public async Task<ActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var userId = User.Identity.GetUserId();
            var items = await _expenditures.GetExpenditures(startDate, endDate, userId);
            var expenditures = new List<ExpenditureViewModel>();
            foreach (var i in items)
            {
                var expenditure = new ExpenditureViewModel
                {
                    Id = i.Id,
                    ExpenseTime = i.ExpenseTime.ToShortDateString(),
                    Comment = i.Comment,
                    Cost = i.Cost,
                    CategoryName = i.CategoryName
                };
                expenditures.Add(expenditure);
            }
            return View(expenditures);
        }

        public async Task<ActionResult> Details(int id)
        {
            var item = await _expenditures.GetExpenditure(id);
            var expenditure = new ExpenditureViewModel
            {
                Id = item.Id,
                ExpenseTime = item.ExpenseTime.ToShortDateString(),
                Comment = item.Comment,
                Cost = item.Cost,
                CategoryName = item.CategoryName
            };
            return View(expenditure);
        }

        public async Task<ActionResult> Create(string id)
        {
            ViewBag.UserId = id;
            await PopulateCategoriesData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseTime, Cost, Comment, CategoryId")]ExpenditureCreateEditViewModel model)
        {
            var id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var expenditure = new ExpenditureCreateEditDTO
                {
                    ExpenseTime = model.ExpenseTime,
                    Cost = model.Cost,
                    Comment = model.Comment,
                    CategoryId = model.CategoryId,
                    UserId = id
                };
                var result = await _expenditures.Add(expenditure);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserInfo", "User", new { id = id });
                }
                ModelState.AddModelError(result.Property, result.Message);
            }
            await PopulateCategoriesData(model.CategoryId);
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _expenditures.GetExpenditureForEdit(id);
            var expenditure = new ExpenditureCreateEditViewModel
            {
                Id = item.Id,
                ExpenseTime = (DateTime)item.ExpenseTime,
                Comment = item.Comment,
                Cost = (decimal)item.Cost,
                CategoryId = (int)item.CategoryId
            };
            await PopulateCategoriesData(expenditure.CategoryId);
            return View(expenditure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExpenditureCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var expenditure = new ExpenditureCreateEditDTO
                {
                    Id = model.Id,
                    ExpenseTime = model.ExpenseTime,
                    Cost = model.Cost,
                    Comment = model.Comment,
                    CategoryId = model.CategoryId
                };
                var result = await _expenditures.Update(expenditure);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError(result.Property, result.Message);
            }
            await PopulateCategoriesData(model.CategoryId);
            return View(model);
        }

        public async Task<ActionResult> Delete(int id, string message)
        {
            var item = await _expenditures.GetExpenditure(id);
            var expenditure = new ExpenditureViewModel
            {
                Id = item.Id,
                ExpenseTime = item.ExpenseTime.ToShortDateString(),
                Comment = item.Comment,
                Cost = item.Cost,
                CategoryName = item.CategoryName
            };
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.ErrorMessage = message;
            }
            return View(expenditure);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(ExpenditureViewModel model)
        {
            var result = await _expenditures.Delete(model.Id);
            if (result.Succeeded)
                return RedirectToAction("Index");
            return RedirectToAction("Delete", new { message = result.Message });
        }

        public async Task PopulateCategoriesData(object selectedCat = null)
        {
            var items = await _categories.GetCategories();
            var categories = new List<CategoryViewModel>();
            foreach (var i in items)
            {
                var category = new CategoryViewModel { Id = i.Id, Name = i.Name };
                categories.Add(category);
            }
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", selectedCat);
        }
    }
}