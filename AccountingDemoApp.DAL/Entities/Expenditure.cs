using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Entities
{
    public class Expenditure
    {
        public int Id { get; set; }
        public DateTime ExpenseTime { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public int CategoryId { get; set; }
        public string ApplicationUserId { get; set; }
        public Category Category { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
