using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            Expenses = new List<decimal?>();
            MainData = new List<TestModel>();
        }
        public ICollection<decimal?> Expenses { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public Month Month { get; set; }
        public ICollection<TestModel> MainData { get; set; }
    }
    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}