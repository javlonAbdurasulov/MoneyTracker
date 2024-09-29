using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public Task<ResponseModel<ResponseModel<List<Income>>>> ApplyFilter(MoneyFilterDTO incomeFilterDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Income>> Create(MoneyDTO incomeDTO)
        {
            Income income = new Income()
            {
                Amount = incomeDTO.Amount,
                Category = incomeDTO.Category,
                Comment = incomeDTO.Comment,
                Date = incomeDTO.Date
            };
            var responseIncome = await _incomeRepository.CreateAsync(income); 
            if (responseIncome == null)
            {
                return new("ошибка при создании");
            }
            return new(responseIncome);
        }

        public async Task<bool> Delete(int incomeId)
        {
            var responseDelete = await _incomeRepository.DeleteAsync(incomeId);
            return responseDelete;
        }

        public async Task<ResponseModel<Income>> GetById(int incomeId)
        {
            var responseIncome = await _incomeRepository.GetById(incomeId);
            if(responseIncome == null)
            {
                return new("Приход с таким Id не существует");
            }
            return new(responseIncome);
        }

        public async Task<ResponseModel<Income>> Update(Income income)
        {
            var incomeById = await GetById(income.Id);
            if (incomeById == null)
            {
                return new(incomeById.Error);
            }
            incomeById.Result = income;
            var responseIncome = await _incomeRepository.UpdateAsync(incomeById.Result); 
            return new(responseIncome);
        }
    }
}
