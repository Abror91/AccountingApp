using AccountingDemoApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Interfaces
{
    public interface IExpenditureRepository : IDisposable
    {
        Task<IEnumerable<Expenditure>> GetExpenditures(DateTime? startDate, DateTime? endDate, string userId);
        Task<Expenditure> GetExpenditure(int? id);
        Task Add(Expenditure expenditure);
        Task Update(Expenditure expenditure);
        Task Delete(Expenditure expenditure);
    }
}
