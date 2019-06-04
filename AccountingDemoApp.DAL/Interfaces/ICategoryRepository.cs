using AccountingDemoApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetCategoriesWithExpenses(string filterByYear, string userId);
        Task<IEnumerable<string>> GetExistingCategoryNamesByYear(string filterByYear, string userId);
        Task<Category> GetCategory(int? id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(Category category);
    }
}
