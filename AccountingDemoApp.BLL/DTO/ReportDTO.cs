using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.DTO
{
    public class ReportDTO
    {
        public ReportDTO()
        {
            Expenses = new List<ExpenditureDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExpenditureDTO> Expenses { get; set; }
    }
}
