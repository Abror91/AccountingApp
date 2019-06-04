using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Interfaces
{
    public interface IExpenditureService : IDisposable
    {
        Task<IEnumerable<ExpenditureDTO>> GetExpenditures(DateTime? startDate, DateTime? endDate, string userId);
        Task<ExpenditureDTO> GetExpenditure(int? id);
        Task<ExpenditureCreateEditDTO> GetExpenditureForEdit(int? id);
        Task<OperationDetails> Add(ExpenditureCreateEditDTO expenditure);
        Task<OperationDetails> Update(ExpenditureCreateEditDTO expenditure);
        Task<OperationDetails> Delete(int? id);
    }
}
