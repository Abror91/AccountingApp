using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<ReportDTO>> GetCategoriesWithExpenses(string filterByYear, string userId);
        Task<IEnumerable<string>> GetExistingCategoryNamesByYear(string filterByYear, string userId);
        Task<CategoryDTO> GetCategory(int? id);
        Task<OperationDetails> Add(CategoryDTO category);
        Task<OperationDetails> Update(CategoryDTO category);
        Task<OperationDetails> Delete(int? id);
    }
}
