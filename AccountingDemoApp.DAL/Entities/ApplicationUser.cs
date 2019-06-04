using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public decimal Deposit { get; set; }
        public ICollection<Expenditure> Expenses { get; set; }
    }
}
