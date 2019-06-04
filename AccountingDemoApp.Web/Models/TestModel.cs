using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class TestModel
    {
        public TestModel()
        {
            Cost = 0m;
        }
        public decimal? Cost { get; set; }
        public string Name { get; set; }
    }
}