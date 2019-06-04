using AccountingDemoApp.DAL.Entities;
using AccountingDemoApp.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Repositories
{
    public class ExpenditureRepository : IExpenditureRepository
    {
        private readonly IApplicationDbContext _db;
        private readonly IDbSet<Expenditure> _expenditures;
        public ExpenditureRepository(IApplicationDbContext db)
        {
            _db = db;
            _expenditures = db.Set<Expenditure>();
        }

        public async Task<IEnumerable<Expenditure>> GetExpenditures(DateTime? startDate, DateTime? endDate, string userId)
        {
            if (startDate != null && endDate != null)
            {
                return await _expenditures.Where(s => s.ExpenseTime >= startDate && s.ExpenseTime <= endDate 
                && s.ApplicationUserId == userId)
                    .Include(s => s.Category).ToListAsync();
            }
            else
            {
                return await _expenditures.Include(s => s.Category).Where(s => s.ApplicationUserId == userId).ToListAsync();
            }
        }

        public async Task<Expenditure> GetExpenditure(int? id)
        {
            return await _expenditures.Where(s => s.Id == id).Include(s => s.Category).Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync();
        }

        public async Task Add(Expenditure expenditure)
        {
            _expenditures.Add(expenditure);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Expenditure expenditure)
        {
            ChangeState(expenditure, EntityState.Modified);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Expenditure expenditure)
        {
            if (!_expenditures.Local.Contains(expenditure))
                _expenditures.Attach(expenditure);
            _expenditures.Remove(expenditure);
            ChangeState(expenditure, EntityState.Deleted);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void ChangeState(Expenditure expenditure, EntityState state)
        {
            _db.Entry(expenditure).State = state;
        }
    }
}
