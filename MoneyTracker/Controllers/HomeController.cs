using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Domain.Models;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace MoneyTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly IBaseTransactionService _transactionService;

        public HomeController(IUserService userService, IIncomeService incomeService, IExpenseService expenseService,IBaseTransactionService transactionService)
        {
            _userService = userService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UpdateTransaction(UpdateTransactionDTO updateTransaction)
        {
            if (updateTransaction.Category == "Income")
            {
                var income = await _incomeService.GetById(updateTransaction.Id);
                var forUpdateIncome = _incomeService.Update(income.Result);
                return View("CreateMoney");
            }
            else
            {

            }

            return View();
        }
        public IActionResult DeleteTransaction(UpdateTransactionDTO DeleteTransaction)
        {
            if (DeleteTransaction.Category == "Income")
            {

            }
            else
            {

            }

            return View();
        }
        public IActionResult CreateMoney(PreCreateview preCreateview)
        {

            return View(preCreateview);
        }
        public async Task<IActionResult> AddTransactions(CreateView Createview)
        {
            MoneyDTO money = new MoneyDTO()
            {
                Amount = Createview.Amount,
                Category = Createview.Category,
                Comment = Createview.Comment,
                Date = Createview.Date,
                UserId = Createview.UserId
            };

            if (Createview.Category == "Income")
            {
                var res = await _incomeService.Create(money);
            }
            else
            {
                var res = await _expenseService.Create(money);

            }

            IndexModel indexModel = new()
            {
                DefaultFilter = true,
                UserName = Createview.UserName
            };
            return RedirectToAction("Dashboard",indexModel);
        }
        public async Task<IActionResult> Dashboard(IndexModel indexModel)
        {
            var responseUser = await _userService.LoginAsync(indexModel.UserName);
            if(responseUser.Result==null) { 
                return View(new ResponseModel<DashboardModel>(responseUser.Error));
            }
             

            if (indexModel.DefaultFilter)
            {
                MoneyFilterDTO defaultFilterDTO = new MoneyFilterDTO()
                {
                    DateEnd = DateTime.MaxValue.ToUniversalTime(),
                    DateStart = DateTime.MinValue.ToUniversalTime(),
                    AmountStart = decimal.MinValue,
                    AmountEnd = decimal.MaxValue,
                    Category = "All",
                    OrderByAmountUp = false,
                    OrderByDateUp = false,
                    UserId =responseUser.Result.Id
                };

                indexModel.MoneyFilter = defaultFilterDTO;
            }
            else
            {
                indexModel.MoneyFilter.DateStart = indexModel.MoneyFilter.DateStart.ToUniversalTime();

                if (indexModel.MoneyFilter.DateEnd == DateTime.MinValue)
                {
                    indexModel.MoneyFilter.DateEnd = DateTime.MaxValue.ToUniversalTime();
                }
                else
                {
                    indexModel.MoneyFilter.DateEnd = indexModel.MoneyFilter.DateEnd.ToUniversalTime();
                }
                if (indexModel.MoneyFilter.AmountEnd == 0)
                {
                    indexModel.MoneyFilter.AmountEnd = decimal.MaxValue;
                }

            }
            ResponseModel<List<TransactionListDTO>> BaseTransactionList = new ResponseModel<List<TransactionListDTO>>("");
            ResponseModel<List<Income>> IncomeList = new ResponseModel<List<Income>>("");
            ResponseModel<List<Expense>> ExpenseList = new ResponseModel<List<Expense>>("");

            if (indexModel.MoneyFilter.Category == "All")
            {
                BaseTransactionList =await _transactionService.ApplyFilterBaseTransactions(indexModel.MoneyFilter);
            }
            else if(indexModel.MoneyFilter.Category == "Income")
            {
                IncomeList = await _incomeService.ApplyFilter(indexModel.MoneyFilter);
            }
            else
            {
                ExpenseList = await _expenseService.ApplyFilter(indexModel.MoneyFilter);
            }
            
            DashboardModel responseDashModel = new DashboardModel()
            {
                User = responseUser.Result,
                Incomes = IncomeList.Result==null ? new() : IncomeList.Result,
                Expenses = ExpenseList.Result == null ? new() : ExpenseList.Result,
                BaseTransactions = BaseTransactionList == null ? new() : BaseTransactionList.Result,
                Filter = indexModel.MoneyFilter
            };

            return View(new ResponseModel<DashboardModel>(responseDashModel));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
