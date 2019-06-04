using AccountingDemoApp.DAL.Entities;
using AccountingDemoApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _db;
        private readonly IDbSet<Category> _categories;
        public CategoryRepository(IApplicationDbContext db)
        {
            _db = db;
            _categories = db.Set<Category>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithExpenses(string filterByYear,string userId)
        {
            IEnumerable<Category> categories = null;
            if (string.IsNullOrWhiteSpace(filterByYear))
            {
                categories = await _categories.Include(s => s.Expenditures)
                    .Where(s => s.Expenditures.Any(e => e.ExpenseTime.Year == DateTime.Now.Year && e.ApplicationUserId == userId)).ToListAsync();
                foreach (var item in categories)
                {
                    item.Expenditures = item.Expenditures.Where(e => e.ExpenseTime.Year == DateTime.Now.Year && e.ApplicationUserId == userId).ToList();
                }
            }
            else
            {
                int filterBy = int.Parse(filterByYear);
                categories = await _categories.Include(s => s.Expenditures)
                    .Where(s => s.Expenditures.Any(e => e.ExpenseTime.Year == filterBy && e.ApplicationUserId == userId)).ToListAsync();
                foreach (var item in categories)
                {
                    item.Expenditures = item.Expenditures.Where(e => e.ExpenseTime.Year == filterBy && e.ApplicationUserId == userId).ToList();
                }
            }
            return categories;
        }

        public async Task<IEnumerable<string>> GetExistingCategoryNamesByYear(string filterByYear, string userId)
        {
            var catNames = new List<string>();
            List<Category> categories = null;
            if (string.IsNullOrWhiteSpace(filterByYear))
            {
                categories = await _categories.Where(s => s.Expenditures.Any(e => e.ExpenseTime.Year == DateTime.Now.Year && e.ApplicationUserId == userId)).ToListAsync();
            }
            else
            {
                int filterBy = int.Parse(filterByYear);
                categories = await _categories.Where(s => s.Expenditures.Any(e => e.ExpenseTime.Year == filterBy && e.ApplicationUserId == userId)).ToListAsync();
            }
            foreach (var i in categories)
            {
                catNames.Add(i.Name);
            }
            return catNames;
        }

        public async Task<Category> GetCategory(int? id)
        {
            return await _categories.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(Category category)
        {
            _categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            ChangeState(category, EntityState.Modified);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            if (!_categories.Local.Contains(category))
                _categories.Attach(category);
            _categories.Remove(category);
            ChangeState(category, EntityState.Deleted);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void ChangeState(Category category, EntityState state)
        {
            _db.Entry(category).State = state;
        }
    }
}
