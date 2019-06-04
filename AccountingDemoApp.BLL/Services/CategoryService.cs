using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Infrastructure;
using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.DAL.Entities;
using AccountingDemoApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _db;
        public CategoryService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var items = await _db.Categories.GetCategories();
            var categories = new List<CategoryDTO>();
            foreach (var i in items)
            {
                var category = new CategoryDTO
                {
                    Id = i.Id,
                    Name = i.Name
                };
                categories.Add(category);
            }
            return categories;
        }

        public async Task<IEnumerable<ReportDTO>> GetCategoriesWithExpenses(string filterByYear, string userId)
        {
            var items = await _db.Categories.GetCategoriesWithExpenses(filterByYear, userId);
            var categories = new List<ReportDTO>();
            foreach (var i in items)
            {
                var category = new ReportDTO
                {
                    Id = i.Id,
                    Name = i.Name
                };
                foreach (var item in i.Expenditures)
                {
                    var expense = new ExpenditureDTO
                    {
                        Id = item.Id,
                        Comment = item.Comment,
                        Cost = item.Cost,
                        ExpenseTime = item.ExpenseTime,
                        CategoryName = i.Name
                    };
                    category.Expenses.Add(expense);
                }
                categories.Add(category);
            }
            return categories;
        }

        public async Task<IEnumerable<string>> GetExistingCategoryNamesByYear(string filterByYear, string userId)
        {
            return await _db.Categories.GetExistingCategoryNamesByYear(filterByYear, userId);
        }

        public async Task<CategoryDTO> GetCategory(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var item = await _db.Categories.GetCategory(id);
            if (item == null)
                throw new NullReferenceException();
            var category = new CategoryDTO
            {
                Id = item.Id,
                Name = item.Name
            };
            return category;
        }

        public async Task<OperationDetails> Add(CategoryDTO category)
        {
            if (category == null)
                return new OperationDetails(false, "Ошибка! Категория затрат не был указан!", "");
            if (string.IsNullOrWhiteSpace(category.Name))
                return new OperationDetails(false, "Ошибка! Укажите название категории!", "");
            var item = new Category { Name = category.Name };
            await _db.Categories.Add(item);
            return new OperationDetails(true, "Новая категория успешно создана!", "");
        }

        public async Task<OperationDetails> Update(CategoryDTO category)
        {
            if (category == null)
                return new OperationDetails(false, "Ошибка! Категория затрат не был указан!", "");
            if (string.IsNullOrWhiteSpace(category.Name))
                return new OperationDetails(false, "Ошибка! Укажите название категории!", "");
            var item = await _db.Categories.GetCategory(category.Id);
            if (item == null)
                return new OperationDetails(false, "Ошибка! Категория для редактирования не была найдена!", "");
            item.Name = category.Name;
            await _db.Categories.Update(item);
            return new OperationDetails(true, "Категория была успешно обновлена!", "");
        }

        public async Task<OperationDetails> Delete(int? id)
        {
            if (id == null)
                return new OperationDetails(false, "Ошибка! Категория не была найдена!", "");
            var item = await _db.Categories.GetCategory(id);
            if (item == null)
                return new OperationDetails(false, "Ошибка! Категория не была найдена!", "");
            await _db.Categories.Delete(item);
            return new OperationDetails(true, "Категория успешно удалено!", "");
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
