using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.BLL.Services;
using AccountingDemoApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Infrastructure
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
        {
            return new UserService(new UnitOfWork());
        }
    }
}
