using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class DepositViewModel
    {
        [DataType(DataType.Currency, ErrorMessage = "Укажите правильный тип данных!")]
        public decimal Amount { get; set; }
    }
}