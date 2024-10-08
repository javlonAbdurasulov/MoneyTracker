﻿using MoneyTracker.Application.Interfaces.Repository;
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
        private readonly IUserService _userService;
        private readonly IFilterService<Income> _filterIncomeService;

        public IncomeService(IIncomeRepository incomeRepository, IUserService userService, IFilterService<Income> filterIncomeService)
        {
            _incomeRepository = incomeRepository;
            _userService = userService;
            _filterIncomeService = filterIncomeService;
        }

        public async Task<ResponseModel<List<Income>>> ApplyFilter(MoneyFilterDTO incomeFilterDTO)
        {
            var query = _incomeRepository.GetQueryable();
            var filterList = await _filterIncomeService.FilterByUser(query, incomeFilterDTO.UserId);  
            filterList = await _filterIncomeService.FilterByCategory(filterList, "Income"); 
            filterList = await _filterIncomeService.FilterByDate(filterList, incomeFilterDTO.DateStart, incomeFilterDTO.DateEnd);
            filterList = await _filterIncomeService.FilterByAmount(filterList, incomeFilterDTO.AmountStart, incomeFilterDTO.AmountEnd);
            if (incomeFilterDTO.OrderBy==1)
            {
                filterList = await _filterIncomeService.OrderByDateUp(filterList);
            }else if (incomeFilterDTO.OrderBy == 2)
            {
                filterList = await _filterIncomeService.OrderByDateDown(filterList);
            }
            else if (incomeFilterDTO.OrderBy == 3)
            {
                filterList = await _filterIncomeService.OrderByAmountUp(filterList);
            }
            else
            {
                filterList = await _filterIncomeService.OrderByAmountDown(filterList);
            }

            var res = await _filterIncomeService.EndFilter(filterList);
            return new(res);
        }

        public async Task<ResponseModel<Income>> Create(MoneyDTO incomeDTO)
        {
            Income income = new Income()
            {
                Amount = incomeDTO.Amount,
                Category = incomeDTO.Category,
                Comment = incomeDTO.Comment,
                Date = incomeDTO.Date.ToUniversalTime(),
                UserId = incomeDTO.UserId
            };
            var responseIncome = await _incomeRepository.CreateAsync(income); 
            if (responseIncome == null)
            {
                return new("ошибка при создании");
            }
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(income.UserId, 0, income.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseIncome);
        }

        public async Task<bool> Delete(int incomeId)
        {
            Income? DeletedIncome = await _incomeRepository.GetById(incomeId);
            if (DeletedIncome == null)
            {
                return false;
            }
            decimal deletedAmount = DeletedIncome.Amount;

            var responseDelete = await _incomeRepository.DeleteAsync(incomeId);
            if(responseDelete)
            {
                //
                var updatedBalanceUser = await _userService.UpdateBalanceAsync(DeletedIncome.UserId, deletedAmount, 0);
                if (updatedBalanceUser == null)
                {
                    return false;
                }
            }
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
            
            var responseIncome = await _incomeRepository.UpdateAsync(income);
            //
            var updatedBalanceUser = await _userService.UpdateBalanceAsync(income.UserId, incomeById.Result.Amount, income.Amount);
            if (updatedBalanceUser == null)
            {
                return new(updatedBalanceUser.Error);
            }
            return new(responseIncome);
        }
    }
}
