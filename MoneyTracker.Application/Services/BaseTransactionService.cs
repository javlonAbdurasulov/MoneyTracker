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
    public class BaseTransactionService : IBaseTransactionService
    {
        private readonly IFilterService<TransactionListDTO> _filterService;

        public BaseTransactionService(IFilterService<TransactionListDTO> filterService)
        {
            _filterService = filterService;
        }


        public async Task<ResponseModel<List<TransactionListDTO>>> ApplyFilterBaseTransactions(MoneyFilterDTO moneyFilterDTO)
        {
            var query = await _filterService.MargeCategory();
            var filterList = await _filterService.FilterByUser(query, moneyFilterDTO.UserId);
            
            filterList = await _filterService.FilterByDate(filterList, moneyFilterDTO.DateStart, moneyFilterDTO.DateEnd);
            filterList = await _filterService.FilterByAmount(filterList, moneyFilterDTO.AmountStart, moneyFilterDTO.AmountEnd);
            if (moneyFilterDTO.OrderBy == 1)
            {
                filterList = await _filterService.OrderByDateUp(filterList);
            }
            else if (moneyFilterDTO.OrderBy == 2)
            {
                filterList = await _filterService.OrderByDateDown(filterList);
            }
            else if (moneyFilterDTO.OrderBy == 3)
            {
                filterList = await _filterService.OrderByAmountUp(filterList);
            }
            else
            {
                filterList = await _filterService.OrderByAmountDown(filterList);
            }

            var res = await _filterService.EndFilter(filterList);
            return new(res);
        }
    }
}
