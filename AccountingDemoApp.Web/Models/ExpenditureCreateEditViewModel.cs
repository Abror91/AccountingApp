using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingDemoApp.Web.Models
{
    public class ExpenditureCreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите время расхода!")]
        public DateTime ExpenseTime { get; set; }

        [Required(ErrorMessage = "Укажите сумму расхода!")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Добавте комментарий!")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Необходимо указать категорию расхода!")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }
    }
}