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
    public class ReportController : Controller
    {
        private readonly IExpenditureService _expenditures;
        private readonly ICategoryService _categories;
        public ReportController()
        {

        }
        public ReportController(IExpenditureService expenditures, ICategoryService categories)
        {
            _expenditures = expenditures;
            _categories = categories;
        }

        public async Task<ActionResult> Index(string filterByYear)
        {
            var userId = User.Identity.GetUserId();
            var items = await _categories.GetCategoriesWithExpenses(filterByYear, userId);
            ViewBag.Cats = await _categories.GetExistingCategoryNamesByYear(filterByYear, userId);
            var expenses = new List<ReportViewModel>();
            for (int i = 1; i <= 12; i++)
            {
                var model = new ReportViewModel();
                model.Month = MonthGenerator(i);
                foreach (var item in items)
                {
                    var testModel = new TestModel();
                    var cost = 0m;
                    var expenditures = item.Expenses.Where(s => s.ExpenseTime.Month == i && s.CategoryName == item.Name).ToList();

                    foreach (var e in expenditures)
                    {
                        if (e != null)
                        {
                            cost += e.Cost;
                            testModel.Cost += e.Cost;
                            testModel.Name = item.Name;
                        }
                        else
                        {
                            cost += 0;
                            testModel.Cost += 0;
                            testModel.Name = item.Name;
                        }
                    }
                    model.Expenses.Add(cost);
                    model.MainData.Add(testModel);
                }
                expenses.Add(model);
            }
            return View(expenses);
        }

        private Month MonthGenerator(int id)
        {
            var months = new List<Month>
            {
                new Month { Id = 1, Name = "Январь" },
                new Month { Id = 2, Name = "Февраль" },
                new Month { Id = 3, Name = "Март" },
                new Month { Id = 4, Name = "Апрель" },
                new Month { Id = 5, Name = "Май" },
                new Month { Id = 6, Name = "Июнь" },
                new Month { Id = 7, Name = "Июль" },
                new Month { Id = 8, Name = "Август" },
                new Month { Id = 9, Name = "Сентябрь" },
                new Month { Id = 10, Name = "Октябрь" },
                new Month { Id = 11, Name = "Ноябрь" },
                new Month { Id = 12, Name = "Декабрь" }
            };
            return months.Where(s => s.Id == id).FirstOrDefault();
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategoriesToDisplay()
        {
            var categories = await _categories.GetCategories();
            var categoriesToDisplay = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                var cat = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };
                categoriesToDisplay.Add(cat);
            }
            return categoriesToDisplay;
        }
    }
}