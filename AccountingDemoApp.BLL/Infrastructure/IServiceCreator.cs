using AccountingDemoApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Infrastructure
{
    public interface IServiceCreator
    {
        IUserService CreateUserService();
    }
}
