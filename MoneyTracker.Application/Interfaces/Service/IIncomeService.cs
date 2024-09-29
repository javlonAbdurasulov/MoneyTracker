using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Interfaces.Service
{
    public interface IIncomeService
    {
        public Task<ResponseModel<Income>> Create(MoneyDTO income);
        public Task<ResponseModel<Income>> Update(Income income);
        public Task<ResponseModel<Income>> GetById(int incomeId);
        public Task<ResponseModel<List<Income>>> ApplyFilter(MoneyFilterDTO incomeFilterDTO);
        public Task<bool> Delete(int incomeId);
    }
}
