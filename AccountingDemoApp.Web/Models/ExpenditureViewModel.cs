using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class ExpenditureViewModel
    {
        public int Id { get; set; }
        public string ExpenseTime { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
        public string CategoryName { get; set; }
    }
}