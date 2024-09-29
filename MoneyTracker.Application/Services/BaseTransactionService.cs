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
            var query = _filterService.MargeCategory();
            var filterList = _filterService.FilterByUser(query, moneyFilterDTO.UserId);
            
            filterList = _filterService.FilterByDate(filterList, moneyFilterDTO.DateStart, moneyFilterDTO.DateEnd);
            filterList = _filterService.FilterByAmount(filterList, moneyFilterDTO.AmountStart, moneyFilterDTO.AmountEnd);
            if (moneyFilterDTO.OrderByDateUp)
            {
                filterList = _filterService.OrderByDateUp(filterList);
            }
            else
            {
                filterList = _filterService.OrderByDateDown(filterList);
            }
            if (moneyFilterDTO.OrderByAmountUp)
            {
                filterList = _filterService.OrderByAmountUp(filterList);
            }
            else
            {
                filterList = _filterService.OrderByAmountDown(filterList);
            }

            var res = _filterService.EndFilter(filterList);
            return new(res);
        }
    }
}
