using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Депозит")]
        public decimal Deposit { get; set; }
    }
}