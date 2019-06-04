using AccountingDemoApp.BLL.DTO;
using AccountingDemoApp.BLL.Infrastructure;
using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.DAL.Entities;
using AccountingDemoApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Services
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IUnitOfWork _db;
        public ExpenditureService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ExpenditureDTO>> GetExpenditures(DateTime? startDate, DateTime? endDate, string userId)
        {
            var expenditures = new List<ExpenditureDTO>();
            var data = await _db.Expenditures.GetExpenditures(startDate, endDate, userId);
            foreach (var d in data)
            {
                var expenditure = new ExpenditureDTO
                {
                    Id = d.Id,
                    ExpenseTime = d.ExpenseTime,
                    Cost = d.Cost,
                    Comment = d.Comment,
                    CategoryName = d.Category.Name
                };
                expenditures.Add(expenditure);
            }
            return expenditures;
        }

        public async Task<ExpenditureDTO> GetExpenditure(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var item = await _db.Expenditures.GetExpenditure(id);
            if (item == null)
                throw new NullReferenceException();
            var expenditure = new ExpenditureDTO
            {
                Id = item.Id,
                ExpenseTime = item.ExpenseTime,
                Cost = item.Cost,
                Comment = item.Comment,
                CategoryName = item.Category.Name
            };
            return expenditure;
        }

        public async Task<ExpenditureCreateEditDTO> GetExpenditureForEdit(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var item = await _db.Expenditures.GetExpenditure(id);
            if (item == null)
                throw new NullReferenceException();
            var expenditure = new ExpenditureCreateEditDTO
            {
                Id = item.Id,
                ExpenseTime = item.ExpenseTime,
                Cost = item.Cost,
                Comment = item.Comment,
                CategoryId = item.CategoryId,
                UserId = item.ApplicationUserId
            };
            return expenditure;
        }

        public async Task<OperationDetails> Add(ExpenditureCreateEditDTO expenditure)
        {
            if (expenditure == null)
                return new OperationDetails(false, "Ошибка! Укажите расход для добавление!", "");
            if (expenditure.ExpenseTime == null)
                return new OperationDetails(false, "Ошибка! Укажите дату и время расхода!", "");
            if (string.IsNullOrWhiteSpace(expenditure.Comment))
                return new OperationDetails(false, "Ошибка! Добавте комментарий на расход!", "");
            if (expenditure.Cost == null)
                return new OperationDetails(false, "Ошибка! Укажите сумму расхода!", "");
            if (expenditure.CategoryId == null)
                return new OperationDetails(false, "Ошидка! Укажите категорию расхода!", "");
            if (expenditure.UserId == null)
                return new OperationDetails(false, "Ошидка! Укажите пользователя!", "");
            var category = await _db.Categories.GetCategory(expenditure.CategoryId);
            if (category == null)
                return new OperationDetails(false, "Ошибка! Выбранная категория не сушествует!", "");
            var user = await _db.UserManager.FindByIdAsync(expenditure.UserId);
            if (user == null)
                return new OperationDetails(false, "Ошибка! Пользователь не был найден!", "");
            if (user.Deposit < expenditure.Cost)
            {
                return new OperationDetails(false, "Ошибка! У вас не достаточно средств на счету!", "");
            }
            user.Deposit -= (decimal)expenditure.Cost;
            await _db.SaveChanges();
            var item = new Expenditure
            {
                ExpenseTime = (DateTime)expenditure.ExpenseTime,
                Comment = expenditure.Comment,
                Cost = (decimal)expenditure.Cost,
                CategoryId = (int)expenditure.CategoryId,
                ApplicationUserId = user.Id
            };
            await _db.Expenditures.Add(item);
            return new OperationDetails(true, "Расход был успешно добавлен!", "");
        }

        public async Task<OperationDetails> Update(ExpenditureCreateEditDTO expenditure)
        {
            if (expenditure == null)
                return new OperationDetails(false, "Ошибка! Укажите расход для редактирование!", "");
            if (expenditure.ExpenseTime == null)
                return new OperationDetails(false, "Ошибка! Укажите дату и время расхода!", "");
            if (string.IsNullOrWhiteSpace(expenditure.Comment))
                return new OperationDetails(false, "Ошибка! Добавте комментарий на расход!", "");
            if (expenditure.Cost == null)
                return new OperationDetails(false, "Ошибка! Укажите сумму расхода!", "");
            if (expenditure.CategoryId == null)
                return new OperationDetails(false, "Ошидка! Укажите категорию расхода!", "");
            var category = await _db.Categories.GetCategory(expenditure.CategoryId);
            if (category == null)
                return new OperationDetails(false, "Ошибка! Выбранная категория не сушествует!", "");
            var item = await _db.Expenditures.GetExpenditure(expenditure.Id);
            if (item == null)
                return new OperationDetails(false, "Ошибка! расход не был найден!", "");
            item.ExpenseTime = (DateTime)expenditure.ExpenseTime;
            item.Cost = (decimal)expenditure.Cost;
            item.Comment = expenditure.Comment;
            item.CategoryId = (int)expenditure.CategoryId;
            await _db.Expenditures.Update(item);
            return new OperationDetails(true, "Расход был успешно обнавлён!", "");

        }

        public async Task<OperationDetails> Delete(int? id)
        {
            if (id == null)
                return new OperationDetails(false, "Ошибка! Расход не был указан для удаление!", "");
            var item = await _db.Expenditures.GetExpenditure(id);
            if (item == null)
                return new OperationDetails(false, "Ошибка! Расход не был найден!", "");
            await _db.Expenditures.Delete(item);
            return new OperationDetails(true, "Расход был успешно удален!", "");
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
